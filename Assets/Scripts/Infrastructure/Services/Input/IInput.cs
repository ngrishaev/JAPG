﻿namespace Infrastructure.Services.Input
{
    public interface IInput : IService
    {
        float HorizontalMovement { get; }
        bool JumpPressedDown { get; }
        bool JumpPressed { get; }
        bool DashPressedDown { get; }
        bool ShootPressedDown { get; }
        bool RocketShootPressedDown { get; }
    }
}