using System;
using Data;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.PersistentProgress.SaveLoad;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            
            if(_progressService.Progress == null)
                throw new Exception("Progress is not initialized"); // TODO: Create custom exception so it's ensure that argument is not null
            
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit() { }

        private void LoadProgressOrInitNew() => 
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgress NewProgress() => 
            new(initialLevel: "Level1");
    }
}