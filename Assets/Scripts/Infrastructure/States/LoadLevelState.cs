using System;
using Game.Hero;
using Game.Room;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.PersistentProgress.SaveLoad;
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

        public LoadLevelState(
            GameStateMachine gameStateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain curtain,
            IGameFactory gameFactory,
            IPersistentProgressService progressService,
            ISaveLoadService saveLoadService
            )
        
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
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
            _saveLoadService.RegisterReader(hero);
            _saveLoadService.RegisterWriter(hero);
        }
    }
}