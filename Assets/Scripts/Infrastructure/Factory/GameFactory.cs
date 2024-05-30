using Infrastructure.AssetsManagement;
using Infrastructure.States;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(GameObject at, LoadLevelState loadLevelState)
        {
            return _assets.Instantiate(AssetsPaths.HeroPath, at.transform.position);
        }
    }
}