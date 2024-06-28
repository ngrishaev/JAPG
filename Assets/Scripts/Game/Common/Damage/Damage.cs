using UnityEngine.Assertions;

namespace Game.Common.Damage
{
    public class Damage
    {
        public int Value { get; }

        public Damage(int value)
        {
            Assert.IsTrue(value > 0, "Damage value should be positive");
            Value = value;
        }
    }
}