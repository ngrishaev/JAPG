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
    }
}