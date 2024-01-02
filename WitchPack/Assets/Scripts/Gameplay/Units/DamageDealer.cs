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
        OnHitTarget += SubscribeStatDamage;
    }


    private void SubscribeStatDamage(Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbility ability, bool crit)
    {
        dmg.AddFlatMod(owner.Stats.BaseDamage);
        if (crit)
        {
            dmg.AddMod((Owner.Stats.CritDamage / 100) + 1);//not sure what the math is supposed to be here - ask gd

        }
    }



}
