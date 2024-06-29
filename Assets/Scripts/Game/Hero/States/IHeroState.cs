namespace Game.Hero.States
{
    public interface IHeroState
    {
        public string Name { get; }
        public void Enter();
        public void Exit();
        public void Update(float deltaTime);
    }
}