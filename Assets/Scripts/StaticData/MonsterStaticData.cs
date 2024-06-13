using System;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "Static Data/Monster Data")]
    public class MonsterStaticData: ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;
        [Range(1, 3)]
        public int Hp;
        [Range(1, 3)]
        public int Damage;
        public GameObject Prefab;
    }
}