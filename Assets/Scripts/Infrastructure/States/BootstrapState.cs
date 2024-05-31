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
        private AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter()
        {

            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit() { }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>("Level1");

        private void RegisterServices()
        {
            _services.RegisterSingle<IInput>(new KeyboardInput());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
        }
    }
}