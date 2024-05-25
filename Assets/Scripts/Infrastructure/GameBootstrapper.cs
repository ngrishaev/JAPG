using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            _ = new Game();

            DontDestroyOnLoad(this);
        }
    }
}
