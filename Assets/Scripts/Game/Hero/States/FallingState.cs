using UnityEngine;

namespace Game.Hero.States
{
    public class FallingState : IHeroState
    {
        private readonly HeroMover _heroMover;
        private readonly HeroAnimations _heroAnimations;
        private readonly Rigidbody2D _rigidbody;

        public string Name => "FallingState";

        public FallingState(HeroMover heroMover, HeroAnimations heroAnimations, Rigidbody2D rigidbody)
        {
            _heroMover = heroMover;
            _heroAnimations = heroAnimations;
            _rigidbody = rigidbody;
        }

        public void Enter()
        {
            _rigidbody.gravityScale = 5f;
            _heroAnimations.PlayFallAnimation();
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