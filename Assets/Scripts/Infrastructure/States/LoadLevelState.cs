using System;
using Game.Enemy;
using Game.Hero;
using Game.Room;
using Infrastructure.Factory;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.PersistentProgress.SaveLoad;
using Infrastructure.Services.Reset;
using Infrastructure.Services.StaticData;
using UI;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPoint = "InitialPoint";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IInput _inputService;
        private readonly IStaticDataService _staticDataService;
        private readonly IResetService _resetService;

        public LoadLevelState(
            GameStateMachine gameStateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain curtain,
            IGameFactory gameFactory,
            IPersistentProgressService progressService,
            ISaveLoadService saveLoadService,
            IInput inputService,
            IStaticDataService staticDataService,
            IResetService resetService
            )
        
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _inputService = inputService;
            _staticDataService = staticDataService;
            _resetService = resetService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _saveLoadService.CleanUp();
            _sceneLoader.Load(sceneName, OnSceneLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnSceneLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            if(_progressService.ProgressData == null)
                throw new Exception("Progress is not initialized"); // TODO: Create custom exception so it's ensure that argument is not null
            
            foreach (var reader in _saveLoadService.ProgressReaders) 
                reader.LoadProgress(_progressService.ProgressData);
        }

        private void InitGameWorld()
        {
            InitHero();
            InitRooms();
            InitSlimes();
        }

        private void InitSlimes()
        {
            var slimes = Object.FindObjectsByType<Slime>(FindObjectsSortMode.None);
            foreach (var slime in slimes)
            {
                slime.Construct(speed: 3, health: 2); // TODO: replace with slime static data
            }
        }

        private void InitRooms()
        {
            var camera = Camera.main;
            Assert.IsNotNull(camera, message: "Main camera is not found");
            
            var rooms = Object.FindObjectsByType<Room>(FindObjectsSortMode.None);
            foreach (var room in rooms)
            {
                room.Construct(camera);
            }
        }

        private void InitHero()
        {
            Hero hero = _gameFactory.CreateHero(at: GameObject.FindGameObjectWithTag(InitialPoint));
            hero.Construct(_inputService, _staticDataService.GetPlayerStaticData());
            
            _saveLoadService.RegisterReader(hero);
            _saveLoadService.RegisterWriter(hero);
        }
    }
}