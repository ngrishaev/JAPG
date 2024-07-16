using Infrastructure.Services.StaticData;
using UnityEngine;

namespace Game.Hero.States
{
    public class FallingState : IHeroState
    {
        private readonly HeroMover _heroMover;
        private readonly HeroAnimations _heroAnimations;
        private readonly Rigidbody2D _rigidbody;
        private readonly HeroStaticData _staticData;

        public string Name => "FallingState";

        public FallingState(
            HeroMover heroMover,
            HeroAnimations heroAnimations,
            Rigidbody2D rigidbody,
            HeroStaticData staticData
            )
        {
            _heroMover = heroMover;
            _heroAnimations = heroAnimations;
            _rigidbody = rigidbody;
            _staticData = staticData;
        }

        public void Enter()
        {
            _rigidbody.gravityScale = _staticData.FallGravity;
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