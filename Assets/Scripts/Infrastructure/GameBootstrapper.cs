using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game = null!;

        private void Awake()
        {
            _game = new Game(this);
            _game.StateMachine.Enter<BootstrappState>();

            DontDestroyOnLoad(this);
        }
    }
}
