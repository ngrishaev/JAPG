using Infrastructure.States;
using UI;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game = null!;
        [SerializeField] private LoadingCurtain _curtain = null!;

        private void Awake()
        {
            _game = new Game(this, _curtain);
            _game.StateMachine.Enter<BootstrappState>();

            DontDestroyOnLoad(this);
        }
    }
}
