using Infrastructure.Services;

namespace StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        MonsterStaticData? GetMonster(MonsterTypeId monsterTypeId);
    }
}