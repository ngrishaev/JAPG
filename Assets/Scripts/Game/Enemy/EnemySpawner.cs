using System;
using Infrastructure.Factory;
using Infrastructure.Services;
using StaticData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private MonsterTypeId _monsterType;

        private string _id;
        private IGameFactory _gameFactory;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }

        public void Spawn()
        {
            var monster = _gameFactory.CreateMonster(_monsterType, transform);
        }
    }
}