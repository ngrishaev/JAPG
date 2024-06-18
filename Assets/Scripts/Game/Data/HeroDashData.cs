using UnityEngine.Assertions;

namespace Game.Data
{
    public class HeroDashData
    {
        private const float CooldownTime = 3f;
        public bool IsDashing { get; private set; }
        public float CurrentCooldownTime { get; private set; }
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
            CurrentCooldownTime = CooldownTime;
        }

        public void ResetAirDash()
        {
            HaveAirDash = true;
        }

        public void StartAirDash()
        {
            Assert.IsTrue(HaveAirDash, "Dash air charge is already spent!");
            
            StartDash();
            HaveAirDash = false;
        }

        public void UpdateCooldown(float deltaTime)
        {
            CurrentCooldownTime -= deltaTime;
        }

        public bool IsCooldownReady() => CurrentCooldownTime <= 0;
    }
}