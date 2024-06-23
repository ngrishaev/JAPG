using System.Collections.Generic;
using Data;
using Game.Data;
using Game.Hero.States;
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
            
            var transitionToDash = new Transition(
                () => _input.DashPressedDown && _groundDetector.IsGrounded && !_heroData.DashData.IsDashing && _heroData.DashData.IsCooldownReady(),
                new DashState(_rigidbody, _heroData.DashData));
            var transitionToAirDash = new Transition(
                () => _input.DashPressedDown && !_groundDetector.IsGrounded && _heroData.DashData is { HaveAirDash: true, IsDashing: false }  && _heroData.DashData.IsCooldownReady(),
                new AirDashState(_rigidbody, _heroData.DashData));
            var transitionToJump = new Transition(
                () => _input.JumpPressedDown && _groundDetector.IsGrounded,
                new JumpState(_input, heroMover, _rigidbody, _animations, _jumpHeight));
            var transitionToGrounded = new Transition(
                () => !_input.JumpPressedDown && _groundDetector.IsGrounded && _rigidbody.velocity.y < 0.001f && !_heroData.DashData.IsDashing,
                groundedState);
            var transitionToAirJump = new Transition(
                () => _input.JumpPressedDown && !_groundDetector.IsGrounded && _heroData.JumpData.HaveAirJump,
                new AirJumpState(_input, heroMover, _rigidbody, _animations, _heroData.JumpData, _jumpHeight));
            var transitionToFalling = new Transition(
                () => _rigidbody.velocity.y < 0 && !_groundDetector.IsGrounded && !_heroData.DashData.IsDashing,
                new FallingState(heroMover, _animations, _rigidbody));
            
            var transitions = new HashSet<Transition>()
            {
                // transitionToDash,
                // transitionToAirDash,
                transitionToJump,
                transitionToGrounded,
                transitionToAirJump,
                transitionToFalling
            };

            return new HeroStateMachine(groundedState, transitions);
        }
    }
}
