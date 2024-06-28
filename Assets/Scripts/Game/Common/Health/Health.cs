using Game.Common.Damage;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Common.Health
{
    public class Health: IDamageable
    {
        public int Value { get; private set; }

        public Health(int value)
        {
            Assert.IsTrue(value > 0, "Health value should be positive");
            Value = value;
        }

        public void ReceiveDamage(Damage.Damage damage)
        {
            Assert.IsTrue(Value > 0, "Health is already zero");
            Value = Mathf.Max(0, Value - damage.Value);
        }
        
        public bool IsDead() => Value <= 0;
    }
}