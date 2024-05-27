namespace Services.Input
{
    public interface IInput
    {
        public float HorizontalMovement { get; }
        public bool JumpPressed { get; }
    }
}