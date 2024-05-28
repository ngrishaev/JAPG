namespace Game.Hero.States
{
    public class GroundedState : IHeroState
    {
        private readonly HeroAnimations _heroAnimations;
        private readonly HeroMover _heroMover;

        public string Name => "GroundedState";

        public GroundedState(
            HeroMover heroMover,
            HeroAnimations heroAnimations)
        {
            _heroAnimations = heroAnimations;
            _heroMover = heroMover;
        }

        public void Enter()
        {
            
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