using Infrastructure.AssetsManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Services.Input;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit() { }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>("Level1");

        private void RegisterServices()
        {
            AllServices.Container.RegisterSingle<IInput>(new KeyboardInput());
            AllServices.Container.RegisterSingle<IAssetProvider>(new AssetProvider());
            AllServices.Container.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssetProvider>()));
        }
    }
}