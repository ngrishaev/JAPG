using Game.Hero;
using Infrastructure.AssetsManagement;
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

        public Hero CreateHero(GameObject at)
        {
            var hero = Instantiate(AssetsPaths.HeroPath, at.transform.position);
            return hero.GetComponent<Hero>();
        }
        
        private GameObject Instantiate(string prefabPath, Vector3 at)
        {
            return _assets.Instantiate(prefabPath, at);
        }
    }
}