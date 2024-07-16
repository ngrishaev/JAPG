using Game.Data;
using Infrastructure.Services.Input;
using Infrastructure.Services.StaticData;
using UnityEngine;

namespace Game.Hero.States
{
    public class AirJumpState : IHeroState
    {
        // TODO - this is basically jump state copypast, need to refactor
        private readonly IInput _input;
        private readonly HeroMover _heroMover;
        private readonly Rigidbody2D _rigidbody;
        private readonly HeroAnimations _heroAnimations;
        private readonly HeroJumpData _jumpData;
        private readonly HeroStaticData _staticData;

        public string Name => "AirJumpState";

        public AirJumpState(
            IInput input,
            HeroMover heroMover,
            Rigidbody2D rigidbody,
            HeroAnimations heroAnimations,
            HeroJumpData jumpData,
            HeroStaticData staticData)
        {
            _input = input;
            _heroMover = heroMover;
            _rigidbody = rigidbody;
            _heroAnimations = heroAnimations;
            _jumpData = jumpData;
            _staticData = staticData;
        }

        public void Enter()
        {
            _jumpData.SpendAirJump();
            
            _rigidbody.gravityScale = _staticData.JumpGravity;
            var jumpSpeed = Mathf.Sqrt(2 * _staticData.JumpHeight * Mathf.Abs(Physics2D.gravity.y * _rigidbody.gravityScale));
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpSpeed);
            _heroAnimations.PlayJumpAnimation();
        }

        public void Exit()
        {
        }

        public void Update(float deltaTime)
        {
            if(!_input.JumpPressed())
                _rigidbody.gravityScale = _staticData.FallGravity;
            _heroMover.UpdateMovement();
        }
    }
}