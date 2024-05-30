using Infrastructure.Services;
using Infrastructure.States;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at, LoadLevelState loadLevelState);
    }
}