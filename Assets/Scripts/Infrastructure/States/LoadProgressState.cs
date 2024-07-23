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
            
            if(_progressService.ProgressData == null)
                throw new Exception("Progress is not initialized"); // TODO: Create custom exception so it's ensure that argument is not null
            
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.ProgressData.WorldData.PositionOnLevel.Level);
        }

        public void Exit() { }

        private void LoadProgressOrInitNew() => 
            _progressService.ProgressData = _saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgress NewProgress() => 
            new(initialLevel: "Level1");
    }
}