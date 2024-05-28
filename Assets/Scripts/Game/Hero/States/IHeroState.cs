namespace Game.Hero.States
{
    public interface IHeroState
    {
        public void Enter();
        public void Exit();
        public void Update(float deltaTime);
        public string Name { get; }
    }
}