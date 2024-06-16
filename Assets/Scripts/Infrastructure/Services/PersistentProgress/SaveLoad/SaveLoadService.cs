using System;
using Data;
using Infrastructure.Factory;
using UnityEngine;

namespace Infrastructure.Services.PersistentProgress.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;
        private const string ProgressKey = "Progress";

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public PlayerProgress? LoadProgress() => 
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();

        public void SaveProgress()
        {
            if(_progressService.Progress == null)
                throw new Exception("Progress is not initialized"); // TODO: Create custom exception so it's ensure that argument is not null
            
            foreach (var writer in _gameFactory.ProgressWriters) 
                writer.WriteProgress(_progressService.Progress);
            
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }
    }
}