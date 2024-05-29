using UnityEngine;

namespace Infrastructure
{
    public interface IGameFactory
    {
        GameObject CreateHero(GameObject at, LoadLevelState loadLevelState);
    }
}