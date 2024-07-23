using Game.Hero;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        Hero CreateHero(GameObject at);
    }
}