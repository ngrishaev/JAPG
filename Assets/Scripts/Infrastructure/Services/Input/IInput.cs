namespace Infrastructure.Services.Input
{
    public interface IInput : IService
    {
        float HorizontalMovement ();
        bool JumpPressedDown ();
        bool JumpPressed ();
        bool Shoot ();
        bool RocketShoot ();
        bool PowerShoot ();
        
    }
}