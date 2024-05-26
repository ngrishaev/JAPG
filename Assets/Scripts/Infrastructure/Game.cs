using Services.Input;

namespace Infrastructure
{
    public class Game
    {
        public static IInput Input { get; set; } = null!;
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner));
        }
    }
}