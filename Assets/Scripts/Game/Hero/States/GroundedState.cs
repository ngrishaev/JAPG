using Game.Data;

namespace Game.Hero.States
{
    public class GroundedState : IHeroState
    {
        private readonly HeroAnimations _heroAnimations;
        private readonly HeroJumpData _jumpData;
        private readonly HeroDashData _dashData;
        private readonly HeroMover _heroMover;

        public string Name => "GroundedState";

        public GroundedState(
            HeroMover heroMover,
            HeroAnimations heroAnimations,
            HeroJumpData jumpData,
            HeroDashData dashData
            )
        {
            _heroAnimations = heroAnimations;
            _jumpData = jumpData;
            _dashData = dashData;
            _heroMover = heroMover;
        }

        public void Enter()
        {
            _jumpData.ResetAirJump();
            _dashData.ResetAirDash();
        }

        public void Update(float deltaTime)
        {
            _heroMover.UpdateMovement();

            if (_heroMover.IsMoving)
                _heroAnimations.PlayRunAnimation();
            else
                _heroAnimations.PlayIdleAnimation();
        }

        public void Exit() { }
    }
}