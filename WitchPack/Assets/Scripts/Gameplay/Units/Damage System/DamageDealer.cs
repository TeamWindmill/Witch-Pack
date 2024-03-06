using System;

public class DamageDealer
{
    private BaseUnit owner;

    public Action<Damageable, DamageDealer, DamageHandler, CastingAbility, bool> OnHitTarget;
    public Action<Damageable, DamageDealer, DamageHandler, CastingAbility, bool> OnKill;
    public Action<Damageable, DamageDealer, DamageHandler, CastingAbility, bool> OnAssist;

    private OffensiveAbility autoAttack;
    public BaseUnit Owner { get => owner; }

    public DamageDealer(BaseUnit owner, OffensiveAbility autoAttack)
    {
        this.owner = owner;
        this.autoAttack = autoAttack;
        OnHitTarget += SubscribeStatDamage;
        OnHitTarget += SubscribeDamageBoostsFromAbility;
    }


    public bool CritChance(BaseAbility ability)
    {
        if (ReferenceEquals(ability, owner.AutoAttack) && UnityEngine.Random.Range(0, 100) <= owner.Stats.CritChance)
        {
            return true;
        }
        return false;
    }

    private void SubscribeStatDamage(Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbility ability, bool crit)
    {
        if (ReferenceEquals(ability, owner.AutoAttack))
        {
            dmg.AddFlatMod(owner.Stats.BaseDamage);
            if (crit)
            {
                float critDamage = (Owner.Stats.CritDamage / 100f) + 1f;
                dmg.AddMod(critDamage);//not sure what the math is supposed to be here - ask gd
            }
        }

    }

    private void SubscribeDamageBoostsFromAbility(Damageable target, DamageDealer dealer, DamageHandler dmg, CastingAbility ability, bool crit)
    {
        if (ability is not OffensiveAbility || ReferenceEquals((ability as OffensiveAbility).DamageBoosts, null))
        {
            return;
        }
        foreach (var item in (ability as OffensiveAbility).DamageBoosts)
        {
            switch (item.Type)
            {
                case DamageBonusType.CurHp:
                    dmg.AddFlatMod(GetModCurHp(item, target));
                    break;
                case DamageBonusType.MissingHp:
                    dmg.AddMod(GetModMissingHp(item, target));
                    break;
            }
        }
    }

    private int GetModCurHp(DamageBoostData boostData, Damageable target)
    {
        if((target.CurrentHp / target.MaxHp) >= (boostData.Threshold / 100))
        {
            float damageBasedOnCurrentHP = target.CurrentHp * (boostData.damageBonus / 100);
            return (int)damageBasedOnCurrentHP;
        }

        return 0;
        
    }

    private float GetModMissingHp(DamageBoostData boostData, Damageable target)
    {
        if ((target.CurrentHp / target.MaxHp) >= (boostData.Threshold / 100))
        {
            return 1 + (boostData.damageBonus / 100);
        }
        return 1;
    }

}
