namespace Game.Common.Damage
{
    public interface IDamageable
    {
        void ReceiveDamage(Damage damage);
    }

    public interface IStunnable
    {
        void Stun(float duration);
    }
}