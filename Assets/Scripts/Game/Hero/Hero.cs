using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Game.Hero.States;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Hero
{
    public class Hero : MonoBehaviour, IProgressWriter, IProgressReader
    {
        [SerializeField] private Rigidbody2D _rigidbody = null!;
        [SerializeField] private HeroAnimations _animations = null!;
        [SerializeField] private GroundDetector _groundDetector = null!;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _speed;
        
        private IInput _input = null!;
        private IHeroState _currentState = null!;
        private Dictionary<Func<bool>, IHeroState> _transitions = new();
        
        private void Awake()
        {
            _input = AllServices.Container.Single<IInput>();
            var heroMover = new HeroMover(_input, _rigidbody, _speed);
            
            var groundedState = new GroundedState(heroMover, _animations);
            _transitions = new Dictionary<Func<bool>, IHeroState>()
            {
                {
                    () => !_input.JumpPressedDown && _groundDetector.CheckIsGrounded() && _rigidbody.velocity.y < 0.001f,
                    groundedState
                },
                
                {
                    () => _input.JumpPressedDown && _groundDetector.CheckIsGrounded(),
                    new JumpState(_input, heroMover, _rigidbody, _animations)
                },
                    
                {
                    () => _rigidbody.velocity.y < 0 && !_groundDetector.CheckIsGrounded(),
                    new FallingState(heroMover, _animations, _rigidbody)
                }
            };
            
            _currentState = groundedState;
        }

        private void Update()
        {
            _currentState.Update(Time.deltaTime);
            
            var nextState = GetNextState();
            if (nextState == null)
            {
                return;
            }
            
            ChangeState(nextState);
        }

        private void ChangeState(IHeroState nextState)
        {
            _currentState.Exit();
            _currentState = nextState;
            _currentState.Enter();
        }

        private IHeroState? GetNextState()
        {
            return _transitions.FirstOrDefault(transition => transition.Key()).Value ?? null;
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

        private static string CurrentLevel() => 
            SceneManager.GetActiveScene().name;
    }
}
