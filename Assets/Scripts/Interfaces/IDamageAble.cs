using System;

public interface IDamageAble
{
    public int Health { get; }
    public bool CanBeDamaged { get; }
    public Team Team { get; }
    public event Action OnTakeDamage;
    public void TakeDamage(int damageAmount);
}
