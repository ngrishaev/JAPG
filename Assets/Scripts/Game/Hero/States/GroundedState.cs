using Game.Data;

namespace Game.Hero.States
{
    public class GroundedState : IHeroState
    {
        private readonly HeroAnimations _heroAnimations;
        private readonly HeroJumpData _jumpData;
        private readonly HeroMover _heroMover;

        public string Name => "GroundedState";

        public GroundedState(
            HeroMover heroMover,
            HeroAnimations heroAnimations,
            HeroJumpData jumpData)
        {
            _heroAnimations = heroAnimations;
            _jumpData = jumpData;
            _heroMover = heroMover;
        }

        public void Enter()
        {
            _jumpData.ResetAirJump();
        }

        public void Update(float deltaTime)
        {
            _heroMover.UpdateMovement();

            if (_heroMover.IsMoving)
                _heroAnimations.PlayRunAnimation();
            else
                _heroAnimations.PlayIdleAnimation();
        }

        public void Exit()
        {
            
        }
    }
}