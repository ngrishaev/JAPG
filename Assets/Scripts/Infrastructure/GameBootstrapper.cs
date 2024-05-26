using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game = null!;

        private void Awake()
        {
            _game = new Game();
            _game.StateMachine.Enter<BootstrappState>();

            DontDestroyOnLoad(this);
        }
    }
}
