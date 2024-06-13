using Game;
using Game.Enemy;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using UI;
using UnityEngine;
using UnityEngine.Assertions;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPoint = "InitialPoint";
        private const string EnemySpawnerTag = "EnemySpawner";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(
            GameStateMachine gameStateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain curtain,
            IGameFactory gameFactory,
            IPersistentProgressService progressService)
        
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.CleanUp();
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
            foreach (var reader in _gameFactory.ProgressReaders) 
                reader.LoadProgress(_progressService.Progress);
        }

        private void InitGameWorld()
        {
            var hero = _gameFactory.CreateHero(at: GameObject.FindGameObjectWithTag(InitialPoint));
            
            InitSpawners();

            Assert.IsNotNull(Camera.main, "Main camera is missing");
            Camera.main.GetComponent<CameraFollower>().SetTarget(hero.transform);
        }

        private void InitSpawners()
        {
            foreach (var spawnerObject in GameObject.FindGameObjectsWithTag(EnemySpawnerTag))
            {
                var spawner = spawnerObject.GetComponent<EnemySpawner>();
                spawner.Spawn();
            }
        }
    }
}