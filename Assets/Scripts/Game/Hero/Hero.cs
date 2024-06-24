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
            
            // var transitionToDash = new Transition(
            //     () => _input.DashPressedDown && _groundDetector.GroundedDetected && !_heroData.DashData.IsDashing && _heroData.DashData.IsCooldownReady(),
            //     new DashState(_rigidbody, _heroData.DashData));
            // var transitionToAirDash = new Transition(
            //     () => _input.DashPressedDown && !_groundDetector.GroundedDetected && _heroData.DashData is { HaveAirDash: true, IsDashing: false }  && _heroData.DashData.IsCooldownReady(),
            //     new AirDashState(_rigidbody, _heroData.DashData));
            
            var transitionToJump = TransitionBuilder.CreateTransition()
                .FromState<GroundedState>(withCondition: () => _input.JumpPressedDown)
                .ToState(new JumpState(_input, heroMover, _rigidbody, _animations, _jumpHeight));

            var transitionToGrounded = TransitionBuilder.CreateTransition()
                .FromState<FallingState>(withCondition: () => _groundDetector.GroundedDetected)
                .ToState(groundedState);

            var transitionToAirJump = TransitionBuilder.CreateTransition()
                .FromState<FallingState>(withCondition: () => _input.JumpPressedDown && _heroData.JumpData.HaveAirJump)
                .ToState(new AirJumpState(_input, heroMover, _rigidbody, _animations, _heroData.JumpData, _jumpHeight));

            Func<bool> shouldClimb = () => _input.HorizontalMovement < 0 && _leftWallDetector.GroundedDetected ||
                                               _input.HorizontalMovement > 0 && _rightWallDetector.GroundedDetected;
            
            var transitionToFalling = TransitionBuilder.CreateTransition()
                .FromState<WallClimbingState>(withCondition: () => !shouldClimb())
                .FromAnyOtherState(withCondition: () => _rigidbody.velocity.y < 0 && !_groundDetector.GroundedDetected)
                .ToState(new FallingState(heroMover, _animations, _rigidbody));
            
            var transitionToClimb = TransitionBuilder.CreateTransition()
                .FromState<JumpState>(shouldClimb)
                .FromState<AirJumpState>(shouldClimb)
                .FromState<FallingState>(ClimbDebug)
                .ToState(new WallClimbingState(_rigidbody));
            
            var transitions = new HashSet<ITransition>()
            {
                // transitionToDash,
                // transitionToAirDash,
                transitionToJump,
                transitionToGrounded,
                transitionToAirJump,
                transitionToFalling,
                transitionToClimb
            };

            return new HeroStateMachine(groundedState, transitions);
        }

        private bool ClimbDebug()
        {
            Debug.Log($"ClimbDebug Left: {_leftWallDetector.GroundedDetected} {_input.HorizontalMovement < 0}");
            Debug.Log($"ClimbDebug Right: {_rightWallDetector.GroundedDetected} {_input.HorizontalMovement > 0}");
            
            return _input.HorizontalMovement < 0 && _leftWallDetector.GroundedDetected ||
                _input.HorizontalMovement > 0 && _rightWallDetector.GroundedDetected;
            
        }
    }
}
