using Services.Input;
using UnityEngine;

namespace Game.Hero.States
{
    public class JumpState : IHeroState
    {
        private readonly IInput _input;
        private readonly Rigidbody2D _rigidbody;
        private readonly GroundDetector _groundDetector;
        private readonly float _jumpForce;
        private readonly HeroAnimations _heroAnimations;

        public string Name => "JumpState";

        public JumpState(IInput input, Rigidbody2D rigidbody, GroundDetector groundDetector, float jumpForce, HeroAnimations heroAnimations)
        {
            _input = input;
            _rigidbody = rigidbody;
            _groundDetector = groundDetector;
            _jumpForce = jumpForce;
            _heroAnimations = heroAnimations;
        }

        public void Enter()
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce);
            _heroAnimations.PlayJumpAnimation();
        }

        public void Exit()
        {
        }

        public void Update(float deltaTime) { }
    }
}