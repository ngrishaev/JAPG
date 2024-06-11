using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private MonsterTypeId _monsterType;
    }
}