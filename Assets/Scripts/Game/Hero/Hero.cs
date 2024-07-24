using System;
using System.Collections.Generic;
using Data;
using Game.Data;
using Game.Hero.Attack;
using Game.Hero.States;
using Game.Hero.Transitions;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Hero
{
    public class Hero : MonoBehaviour, IProgressWriter, IProgressReader
    {
        [SerializeField] private Rigidbody2D _rigidbody = null!;
        [SerializeField] private HeroAnimations _animations = null!;
        [SerializeField] private GroundDetector _groundDetector = null!;
        [SerializeField] private GroundDetector _leftWallDetector = null!;
        [SerializeField] private GroundDetector _rightWallDetector = null!;
        [SerializeField] private HeroShooter _shooter = null!;
        
        private IInput _input = null!;
        private HeroStateMachine _stateMachine = null!;
        private HeroData _heroData = null!;
        private HeroStaticData _heroStaticData = null!;

        public void Construct(IInput input, HeroStaticData staticData)
        {
            _input = input;
            _heroStaticData = staticData;
                        
            _heroData = new HeroData(); 
                        
            _stateMachine = CreatePlayerStateMachine();
        }

        private void Update()
        {
            _stateMachine.Update(Time.deltaTime);
            _heroData.DashData.UpdateCooldown(Time.deltaTime);
            
            if(_input.Shoot())
                _shooter.TryShootBullet();
            
            if(_input.RocketShoot())
                _shooter.TryShootRocket();
            
            if(_input.PowerShoot())
                _shooter.TryShootStun();
        }

        public void WriteProgress(PlayerProgress progress) => 
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVector3Data());

        public void LoadProgress(PlayerProgress progress)
        {
            if(!CurrentLevel().Equals(progress.WorldData.PositionOnLevel.Level))
                return;
            
            var savedPosition = progress.WorldData.PositionOnLevel.Position;
            if (savedPosition == null)
                return;
            
            transform.position = savedPosition.AsVector3();
        }

        public void Damage()
        {
            Debug.Log("Hero was damaged!");
        }

        private static string CurrentLevel() => 
            SceneManager.GetActiveScene().name;

        private HeroStateMachine CreatePlayerStateMachine()
        {
            var heroMover = new HeroMover(_input, _rigidbody, _heroStaticData.Speed);
            var groundedState = new GroundedState(heroMover, _animations, _heroData.JumpData, _heroData.DashData);
            
            var transitionToJump = TransitionBuilder.CreateTransition()
                .FromState<GroundedState>(withCondition: () => _input.JumpPressedDown())
                .ToState(new JumpState(_input, heroMover, _rigidbody, _animations, _heroStaticData));

            var transitionToGrounded = TransitionBuilder.CreateTransition()
                .FromState<FallingState>(withCondition: () => _groundDetector.GroundedDetected)
                .ToState(groundedState);

            Func<bool> requestingAirJump = () => _input.JumpPressedDown() && _heroData.JumpData.HaveAirJump;
            var transitionToAirJump = TransitionBuilder.CreateTransition()
                .FromState<FallingState>(withCondition: requestingAirJump)
                .FromState<JumpState>(withCondition: requestingAirJump)
                .ToState(new AirJumpState(_input, heroMover, _rigidbody, _animations, _heroData.JumpData, _heroStaticData));

            Func<bool> isRequestingClimb = () => _input.HorizontalMovement() < 0 && _leftWallDetector.GroundedDetected ||
                                               _input.HorizontalMovement() > 0 && _rightWallDetector.GroundedDetected;
            
            var transitionToFalling = TransitionBuilder.CreateTransition()
                .FromState<WallClimbingState>(withCondition: () => !isRequestingClimb())
                .FromAnyOtherState(withCondition: () => _rigidbody.velocity.y < 0 && !_groundDetector.GroundedDetected)
                .ToState(new FallingState(heroMover, _animations, _rigidbody, _heroStaticData));
            
            var transitionToClimb = TransitionBuilder.CreateTransition()
                .FromState<FallingState>(isRequestingClimb)
                .ToState(new WallClimbingState(_rigidbody));
            
            var transitionToWallJump = TransitionBuilder.CreateTransition()
                .FromState<WallClimbingState>(() => _input.JumpPressedDown())
                .ToState(new WallJumpState(_rigidbody, _heroStaticData));
            
            var transitions = new HashSet<ITransition>()
            {
                transitionToJump,
                transitionToGrounded,
                transitionToAirJump,
                transitionToFalling,
                transitionToClimb,
                transitionToWallJump
            };

            return new HeroStateMachine(groundedState, transitions);
        }
    }
}
