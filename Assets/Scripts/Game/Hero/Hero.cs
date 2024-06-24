using System;
using System.Collections.Generic;
using Data;
using Game.Data;
using Game.Hero.States;
using Game.Hero.Transitions;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
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
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpHeight;
        
        private IInput _input = null!;
        private HeroStateMachine _stateMachine = null!;
        private HeroData _heroData = null!;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInput>();
            _heroData = new HeroData(); // TODO - load in load level state;
            
            _stateMachine = CreatePlayerStateMachine();
        }

        private void Update()
        {
            _stateMachine.Update(Time.deltaTime);
            _heroData.DashData.UpdateCooldown(Time.deltaTime);
            if(_input.ShootPressedDown)
                _shooter.TryShoot();
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
            var heroMover = new HeroMover(_input, _rigidbody, _speed);
            var groundedState = new GroundedState(heroMover, _animations, _heroData.JumpData, _heroData.DashData);
            
            var transitionToJump = TransitionBuilder.CreateTransition()
                .FromState<GroundedState>(withCondition: () => _input.JumpPressedDown)
                .ToState(new JumpState(_input, heroMover, _rigidbody, _animations, _jumpHeight));

            var transitionToGrounded = TransitionBuilder.CreateTransition()
                .FromState<FallingState>(withCondition: () => _groundDetector.GroundedDetected)
                .ToState(groundedState);

            Func<bool> requestingAirJump = () => _input.JumpPressedDown && _heroData.JumpData.HaveAirJump;
            var transitionToAirJump = TransitionBuilder.CreateTransition()
                .FromState<FallingState>(withCondition: requestingAirJump)
                .FromState<JumpState>(withCondition: requestingAirJump)
                .ToState(new AirJumpState(_input, heroMover, _rigidbody, _animations, _heroData.JumpData, _jumpHeight));

            Func<bool> requestingClimb = () => _input.HorizontalMovement < 0 && _leftWallDetector.GroundedDetected ||
                                               _input.HorizontalMovement > 0 && _rightWallDetector.GroundedDetected;
            
            var transitionToFalling = TransitionBuilder.CreateTransition()
                .FromState<WallClimbingState>(withCondition: () => !requestingClimb())
                .FromAnyOtherState(withCondition: () => _rigidbody.velocity.y < 0 && !_groundDetector.GroundedDetected)
                .ToState(new FallingState(heroMover, _animations, _rigidbody));
            
            var transitionToClimb = TransitionBuilder.CreateTransition()
                .FromState<FallingState>(requestingClimb)
                .ToState(new WallClimbingState(_rigidbody));
            
            var transitions = new HashSet<ITransition>()
            {
                transitionToJump,
                transitionToGrounded,
                transitionToAirJump,
                transitionToFalling,
                transitionToClimb
            };

            return new HeroStateMachine(groundedState, transitions);
        }
    }
}
