﻿using Services.Input;

namespace Infrastructure
{
    public class Game
    {
        public static IInput Input { get; private set; }

        public Game()
        {
            Input = new KeyboardInput();
        }
    }
}