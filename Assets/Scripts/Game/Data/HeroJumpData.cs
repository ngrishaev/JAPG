using UnityEngine.Assertions;

namespace Game.Data
{
    public class HeroJumpData
    {
        public bool HaveAirJump { get; private set; }
        
        public void ResetAirJump()
        {
            HaveAirJump = true;
        }

        public void SpendAirJump()
        {
            Assert.IsTrue(HaveAirJump, "Jump charge is already spent!");
            
            HaveAirJump = false;
        }
    }
    
    public class HeroData
    {
        public HeroJumpData JumpData { get; private set; } = new HeroJumpData();

    }
}