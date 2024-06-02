using System.Collections.Generic;
using Infrastructure.AssetsManagement;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.States;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<IProgressReader> ProgressReaders { get; } = new();
        public List<IProgressWriter> ProgressWriters { get; } = new();
        
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(GameObject at, LoadLevelState loadLevelState) => 
            InstantiateRegistred(AssetsPaths.HeroPath, at.transform.position);

        private GameObject InstantiateRegistred(string prefabPath, Vector3 at)
        {
            var gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        
        private GameObject Instantiate(string prefabPath, Vector3 at)
        {
            return _assets.Instantiate(prefabPath, at);
        }

        private void Register(IProgressWriter reader) => 
            ProgressWriters.Add(reader);

        private void Register(IProgressReader reader) => 
            ProgressReaders.Add(reader);

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (var reader in gameObject.GetComponentsInChildren<IProgressReader>()) 
                Register(reader);

            foreach (var reader in gameObject.GetComponentsInChildren<IProgressWriter>()) 
                Register(reader);
        }
    }
}