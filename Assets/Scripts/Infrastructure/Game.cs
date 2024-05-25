using Services.Input;

namespace Infrastructure
{
    public class Game
    {
        public static IInput Input { get; private set; } = null!;

        public Game()
        {
            Input = new KeyboardInput();
        }
    }
}