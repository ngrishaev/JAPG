using UnityEngine.Assertions;

namespace Game.Data
{
    public class HeroDashData
    {
        public bool IsDashing { get; private set; }
        public float CooldownTime { get; private set; }
        public bool HaveAirDash { get; private set; }

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
        
        public void ResetAirDash()
        {
            HaveAirDash = true;
        }
        
        public void SpendAirDash()
        {
            Assert.IsTrue(HaveAirDash, "Dash charge is already spent!");
            
            HaveAirDash = false;
        }

        public void UpdateCooldown(float deltaTime)
        {
            CooldownTime -= deltaTime;
        }
    }
}