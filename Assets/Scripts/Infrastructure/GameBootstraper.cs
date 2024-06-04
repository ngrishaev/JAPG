using Infrastructure.States;
using UI;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstraper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game = null!;
        [SerializeField] private LoadingCurtain _curtain = null!;

        private void Awake()
        {
            _game = new Game(this, Instantiate(_curtain));
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}
