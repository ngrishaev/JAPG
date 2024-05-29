using UnityEngine;

namespace Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private const string HeroPath = "Hero";

        public GameObject CreateHero(GameObject at, LoadLevelState loadLevelState)
        {
            return Instantiate(HeroPath, at.transform.position);
        }

        private static GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
    }
}