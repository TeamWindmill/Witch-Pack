using System;

public class DamageDealer
{
    private BaseUnit owner;

    public Action<Damageable, DamageDealer, DamageHandler, CastingAbility, bool> OnHitTarget;
    public Action<Damageable, DamageDealer, DamageHandler, CastingAbility, bool> OnKill;
    public Action<Damageable, DamageDealer, DamageHandler, CastingAbility, bool> OnAssist;

    private OffensiveAbilitySO autoAttack;

    public BaseUnit Owner
    {
        get => owner;
    }

    public DamageDealer(BaseUnit owner, OffensiveAbilitySO autoAttack)
    {
        this.owner = owner;
        this.autoAttack = autoAttack;
        OnHitTarget += SubscribeStatDamage;
        OnHitTarget += SubscribeDamageBoostsFromAbility;
    }


    public bool CritChance(Ability ability)
    {
        if (ReferenceEquals(ability, owner.AutoAttackCaster.Ability) && UnityEngine.Random.Range(0, 100) <= owner.Stats[StatType.CritChance].Value)
        {
            return true;
        }

        return false;
    }

    private void SubscribeStatDamage(Damageable target, DamageDealer dealer, DamageHandler dmg, Ability ability, bool crit)
    {
        if (ReferenceEquals(ability, owner.AutoAttackCaster.Ability))
        {
            dmg.AddFlatMod(owner.Stats[StatType.BaseDamage].IntValue);
            if (crit)
            {
                float critDamage = (Owner.Stats[StatType.CritDamage].Value / 100f) + 1f;
                dmg.AddMod(critDamage); //not sure what the math is supposed to be here - ask gd
            }
        }
    }

    private void SubscribeDamageBoostsFromAbility(Damageable target, DamageDealer dealer, DamageHandler dmg, CastingAbility ability, bool crit)
    {
        if (ability is not OffensiveAbility offensiveAbility) return;

        foreach (var item in offensiveAbility.DamageBoosts)
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
        float damageBasedOnCurrentHP = target.CurrentHp * (boostData.damageBonusInPercent / 100);
        return (int)damageBasedOnCurrentHP;
    }

    private float GetModMissingHp(DamageBoostData boostData, Damageable target)
    {
        var missingHp = (target.MaxHp - target.CurrentHp);
        float damageBasedOnMissingHp = missingHp * (boostData.damageBonusInPercent / 100);
        return (int)damageBasedOnMissingHp;
    }
}