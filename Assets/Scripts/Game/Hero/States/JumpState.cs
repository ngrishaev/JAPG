using UnityEngine;

namespace Game.Hero.States
{
    public class JumpState : IHeroState
    {
        private readonly HeroMover _heroMover;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _jumpForce;
        private readonly HeroAnimations _heroAnimations;

        public string Name => "JumpState";

        public JumpState(HeroMover heroMover, Rigidbody2D rigidbody, float jumpForce, HeroAnimations heroAnimations)
        {
            _heroMover = heroMover;
            _rigidbody = rigidbody;
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

        public void Update(float deltaTime)
        {
            _heroMover.UpdateMovement();
        }
    }
}