namespace Game.Hero.Attack
{
    public class CooldownChecker
    {
        private readonly float _cooldown;

        private float _currentCooldown;

        public CooldownChecker(float cooldown)
        {
            _cooldown = cooldown;
        }

        public bool IsCooldownPassed()
        {
            return !(_currentCooldown > 0);
        }
        
        public void Update(float deltaTime)
        {
            if (_currentCooldown > 0)
                _currentCooldown -= deltaTime;
        }
        
        public void ResetCooldown()
        {
            _currentCooldown = _cooldown;
        }
    }
}