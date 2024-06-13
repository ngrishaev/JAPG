using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters = new();

        public void LoadMonsters()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>("StaticData")
                .ToDictionary(x => x.MonsterTypeId, x => x);
        }

        public MonsterStaticData? GetMonster(MonsterTypeId monsterTypeId)
        {
            return _monsters.GetValueOrDefault(monsterTypeId);
        }
    }
}