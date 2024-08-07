﻿using Infrastructure.AssetsManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.PersistentProgress.SaveLoad;
using Infrastructure.Services.Reset;
using Infrastructure.Services.StaticData;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);
        }

        public void Exit() { }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            _services.RegisterSingle<IStaticDataService>(new StaticDataService());
            _services.RegisterSingle<IInput>(new KeyboardInput());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
            _services.RegisterSingle<IResetService>(new ResetService());
        }
    }
}