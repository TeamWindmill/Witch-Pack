using Sirenix.OdinInspector.Editor;
using System;

[System.Serializable]
public class DamageDealer
{
    private BaseUnit owner;

    public Action<Damageable, DamageDealer, DamageHandler, BaseAbility, bool> OnHitTarget;
    public Action<Damageable, DamageDealer, DamageHandler, BaseAbility> OnKill;

    private OffensiveAbility autoAttack;
    public BaseUnit Owner { get => owner; }

    public DamageDealer(BaseUnit owner, OffensiveAbility autoAttack)
    {
        this.owner = owner;
        this.autoAttack = autoAttack;
        OnHitTarget += SubscribeStatDamage;
    }


    private void SubscribeStatDamage(Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbility ability, bool crit)
    {
        if (ReferenceEquals(ability, owner.AutoAttack))
        {
            dmg.AddFlatMod(owner.Stats.BaseDamage);
            if (crit)
            {
                dmg.AddMod((Owner.Stats.CritDamage / 100) + 1);//not sure what the math is supposed to be here - ask gd
            }
        }
        
    }
}
