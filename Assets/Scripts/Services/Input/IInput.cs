namespace Services.Input
{
    public interface IInput
    {
        float HorizontalMovement { get; }
        bool JumpPressedDown { get; }
        bool JumpPressed { get; }
    }
}