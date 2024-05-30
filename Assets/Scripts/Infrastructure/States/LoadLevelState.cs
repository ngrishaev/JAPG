using Game;
using Infrastructure.Factory;
using UI;
using UnityEngine;
using UnityEngine.Assertions;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string Initialpoint = "InitialPoint";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnSceneLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnSceneLoaded()
        {
            var hero = _gameFactory.CreateHero(at: GameObject.FindGameObjectWithTag(Initialpoint), this);

            Assert.IsNotNull(Camera.main, "Main camera is missing");
            Camera.main.GetComponent<CameraFollower>().SetTarget(hero.transform);
            
            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}