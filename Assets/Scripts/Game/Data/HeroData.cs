namespace Game.Data
{
    public class HeroData
    {
        public HeroJumpData JumpData { get; private set; } = new HeroJumpData();
        public HeroDashData DashData { get; private set; } = new HeroDashData();

    }
}