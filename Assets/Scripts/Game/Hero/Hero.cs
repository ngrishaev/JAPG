using System;
using System.Collections.Generic;
using System.Linq;
using Game.Hero.States;
using Services.Input;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Hero
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody = null!;
        [SerializeField] private HeroAnimations _animations = null!;
        [SerializeField] private GroundDetector _groundDetector = null!;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _speed;
        
        private IInput _input = null!;
        private IHeroState _currentState = null!;
        private Dictionary<Func<bool>, IHeroState> _transitions = new Dictionary<Func<bool>, IHeroState>();
        
        public string CurrentStateName = string.Empty;
        

        private void Awake()
        {
            _input = Infrastructure.Game.Input;
            
            var groundedState = new GroundedState(_input, _rigidbody, _animations, transform, _speed);
            _transitions = new Dictionary<Func<bool>, IHeroState>()
            {
                {
                    () => !_input.JumpPressed && _groundDetector.CheckIsGrounded() && _rigidbody.velocity.y < float.Epsilon,
                    groundedState
                },
                
                {
                    () => _input.JumpPressed && _groundDetector.CheckIsGrounded(),
                    new JumpState(_input, _rigidbody, _groundDetector, _jumpForce, _animations)
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
            
            _currentState.Exit();
            _currentState = nextState;
            _currentState.Enter();
            
            CurrentStateName = _currentState.Name;
        }

        private IHeroState? GetNextState()
        {
            return _transitions.FirstOrDefault(transition => transition.Key()).Value ?? null;
        }
    }
}
