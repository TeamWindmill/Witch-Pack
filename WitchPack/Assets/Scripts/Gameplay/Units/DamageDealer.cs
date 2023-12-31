using System;

[System.Serializable]
public class DamageDealer
{
    private BaseUnit owner;

    public Action<Damageable, DamageDealer, DamageHandler, BaseAbility, bool /*critical - a more generic callback*/ > OnHitTarget;
    public Action<Damageable, DamageDealer, DamageHandler, BaseAbility> OnKill;


    public BaseUnit Owner { get => owner; }

    public DamageDealer(BaseUnit owner)
    {
        this.owner = owner;
    }






}
