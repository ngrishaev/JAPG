namespace Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        public HeroStaticData GetPlayerStaticData()
        {
            return new HeroStaticData();
        }
    }
    
    public interface IStaticDataService : IService
    {
        HeroStaticData GetPlayerStaticData();
    }

    public class HeroStaticData
    {
        public readonly float Speed = 5f;        
        public readonly float JumpHeight = 2.2f;   
        public readonly float JumpGravity = 3f;
        public readonly float FallGravity = 5f;
        public readonly float WallJumpHeight = 3f;
        public readonly float WallJumpHorizontalForce = 4f;
        public readonly HeroPistolStaticData PistolStaticData = new();
        public readonly HeroRocketStaticData RocketStaticData = new();
        public readonly HeroStunStaticData StunStaticData = new();
    }

    public class HeroPistolStaticData
    {
        public readonly float Speed = 10f;
        public readonly int Damage = 1;
        public readonly float Cooldown = 0.3f;
    }
    
    public class HeroRocketStaticData
    {
        public readonly float Speed = 5f;
        public readonly int Damage = 3;
        public readonly float Cooldown = 1f;
    }
    
    public class HeroStunStaticData
    {
        public readonly float Speed = 10f;
        public readonly float Duration = 1f;
        public readonly float Cooldown = 1f;
    }
}