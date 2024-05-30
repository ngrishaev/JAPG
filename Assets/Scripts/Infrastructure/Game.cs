using Infrastructure.States;
using Services.Input;
using UI;

namespace Infrastructure
{
    public class Game
    {
        public static IInput Input { get; set; } = null!;
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain);
        }
    }
}