using UnityEngine;

public interface IDamagable
{
    public UnitStats Stats { get;}
    public Effectable Effectable { get;}
    public Affector Affector { get;}
    public Damageable Damageable { get;}
    public void ClearUnitTimers();
    public BaseEntity GameObject { get; }
}