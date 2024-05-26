namespace Infrastructure
{
    public class BootstrappState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public BootstrappState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            RegisterServices();
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            Game.Input = new Services.Input.KeyboardInput();
        }
    }
}