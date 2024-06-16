using UnityEngine;

namespace Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstraper _bootstrap = null!;
        
        private void Awake()
        {
            var bootstraper = FindObjectOfType<GameBootstraper>();

            if (bootstraper)
                return;

            Instantiate(_bootstrap);
        }
    }
}