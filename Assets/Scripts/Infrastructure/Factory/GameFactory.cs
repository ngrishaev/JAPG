using System;
using System.Collections.Generic;
using Game.Enemy;
using Infrastructure.AssetsManagement;
using Infrastructure.Services.PersistentProgress;
using StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<IProgressReader> ProgressReaders { get; } = new();
        public List<IProgressWriter> ProgressWriters { get; } = new();
        public GameObject? Hero { get; private set; } = null;
        
        public event Action<GameObject>? OnHeroCreated;

        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;

        public GameFactory(IAssetProvider assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public GameObject CreateHero(GameObject at)
        {
            var hero = InstantiateRegistred(AssetsPaths.HeroPath, at.transform.position);
            Hero = hero;
            OnHeroCreated?.Invoke(hero);
            return hero;
        }

        public GameObject CreateMonster(MonsterTypeId typeId, Transform parent)
        {
            MonsterStaticData? monsterData = _staticData.GetMonster(typeId);
            var monster = Object.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity).GetComponent<Enemy>();
            monster.Construct(Hero, monsterData.Hp, monsterData.Damage);
            return monster.gameObject;
        }

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