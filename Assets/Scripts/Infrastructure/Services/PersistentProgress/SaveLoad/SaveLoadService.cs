using System;
using System.Collections.Generic;
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
        
        public List<IProgressReader> ProgressReaders { get; } = new();
        public List<IProgressWriter> ProgressWriters { get; } = new();

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public PlayerProgress? LoadProgress() => 
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();

        public void SaveProgress()
        {
            if(_progressService.ProgressData == null)
                throw new Exception("Progress is not initialized"); // TODO: Create custom exception so it's ensure that argument is not null
            
            foreach (var writer in ProgressWriters) 
                writer.WriteProgress(_progressService.ProgressData);
            
            PlayerPrefs.SetString(ProgressKey, _progressService.ProgressData.ToJson());
        }

        public void RegisterWriter(IProgressWriter reader) => 
            ProgressWriters.Add(reader);

        public void RegisterReader(IProgressReader reader) => 
            ProgressReaders.Add(reader);

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}