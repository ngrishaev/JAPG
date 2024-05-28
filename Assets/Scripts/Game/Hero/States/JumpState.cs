using Services.Input;
using UnityEngine;

namespace Game.Hero.States
{
    public class JumpState : IHeroState
    {
        private readonly IInput _input;
        private readonly HeroMover _heroMover;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _jumpForce;
        private readonly HeroAnimations _heroAnimations;
        private readonly AnimationCurve _jumpAnimationCurve;
        private readonly AnimationCurve _fallAnimationCurve;
        private float _jumpTime;
        private float _jumpHeight = 3f;

        public string Name => "JumpState";

        public JumpState(IInput input, HeroMover heroMover, Rigidbody2D rigidbody, float jumpForce,
            HeroAnimations heroAnimations, AnimationCurve jumpAnimationCurve, AnimationCurve fallAnimationCurve)
        {
            _input = input;
            _heroMover = heroMover;
            _rigidbody = rigidbody;
            _jumpForce = jumpForce;
            _heroAnimations = heroAnimations;
            _jumpAnimationCurve = jumpAnimationCurve;
            _fallAnimationCurve = fallAnimationCurve;
        }

        public void Enter()
        {
            _rigidbody.gravityScale = 3f;
            var jumpSpeed = Mathf.Sqrt(2 * _jumpHeight * Mathf.Abs(Physics2D.gravity.y * _rigidbody.gravityScale));
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpSpeed);
            _heroAnimations.PlayJumpAnimation();
        }

        public void Exit()
        {
        }

        public void Update(float deltaTime)
        {
            _heroMover.UpdateMovement();
            if (!_input.JumpPressed)
            {
                _rigidbody.gravityScale = 5f;
                
            }
        }
    }
}