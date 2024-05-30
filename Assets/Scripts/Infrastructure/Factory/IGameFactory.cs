using Infrastructure.States;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory
    {
        GameObject CreateHero(GameObject at, LoadLevelState loadLevelState);
    }
}