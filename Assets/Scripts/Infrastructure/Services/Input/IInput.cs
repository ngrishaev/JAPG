using Infrastructure.Services;

namespace Services.Input
{
    public interface IInput : IService
    {
        float HorizontalMovement { get; }
        bool JumpPressedDown { get; }
        bool JumpPressed { get; }
    }
}