using Infrastructure.Services.Input;
using Infrastructure.Services.StaticData;
using UnityEngine;

namespace Game.Hero.States
{
    public class JumpState : IHeroState
    {
        private readonly IInput _input;
        private readonly HeroMover _heroMover;
        private readonly Rigidbody2D _rigidbody;
        private readonly HeroAnimations _heroAnimations;
        private HeroStaticData _heroStaticData;

        public string Name => "JumpState";

        public JumpState(
            IInput input,
            HeroMover heroMover,
            Rigidbody2D rigidbody,
            HeroAnimations heroAnimations,
            HeroStaticData heroStaticData)
        {
            _input = input;
            _heroMover = heroMover;
            _rigidbody = rigidbody;
            _heroAnimations = heroAnimations;
            _heroStaticData = heroStaticData;
        }

        public void Enter()
        {
            _rigidbody.gravityScale = _heroStaticData.JumpGravity;
            var jumpSpeed = Mathf.Sqrt(2 * _heroStaticData.JumpHeight * Mathf.Abs(Physics2D.gravity.y * _rigidbody.gravityScale));
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpSpeed);
            _heroAnimations.PlayJumpAnimation();
        }

        public void Exit()
        {
        }

        public void Update(float deltaTime)
        {
            if(!_input.JumpPressed())
                _rigidbody.gravityScale = _heroStaticData.FallGravity;
            _heroMover.UpdateMovement();
        }
    }
}