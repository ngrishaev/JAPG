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
    
    public class HeroDashData
    {
        public bool IsDashing { get; private set; }
        public float CooldownTime { get; private set; }

        public HeroDashData()
        {
            IsDashing = false;
        }

        public void StartDash()
        {
            Assert.IsFalse(IsDashing, "Dash is already in progress!");
            
            IsDashing = true;
        }
        
        public void EndDash()
        {
            Assert.IsTrue(IsDashing, "Dash is not in progress!");
            
            IsDashing = false;
        }

        public void UpdateCooldown(float deltaTime)
        {
            CooldownTime -= deltaTime;
        }
    }
}