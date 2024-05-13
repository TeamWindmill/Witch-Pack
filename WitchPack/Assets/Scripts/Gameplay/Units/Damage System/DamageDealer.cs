using System;

public class DamageDealer
{
    private BaseUnit owner;

    public Action<Damageable, DamageDealer, DamageHandler, CastingAbilitySO, bool> OnHitTarget;
    public Action<Damageable, DamageDealer, DamageHandler, CastingAbilitySO, bool> OnKill;
    public Action<Damageable, DamageDealer, DamageHandler, CastingAbilitySO, bool> OnAssist;

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


    public bool CritChance(BaseAbilitySO abilitySo)
    {
        if (ReferenceEquals(abilitySo, owner.AutoAttack) && UnityEngine.Random.Range(0, 100) <= owner.Stats.CritChance)
        {
            return true;
        }

        return false;
    }

    private void SubscribeStatDamage(Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbilitySO abilitySo, bool crit)
    {
        if (ReferenceEquals(abilitySo, owner.AutoAttack))
        {
            dmg.AddFlatMod(owner.Stats.BaseDamage);
            if (crit)
            {
                float critDamage = (Owner.Stats.CritDamage / 100f) + 1f;
                dmg.AddMod(critDamage); //not sure what the math is supposed to be here - ask gd
            }
        }
    }

    private void SubscribeDamageBoostsFromAbility(Damageable target, DamageDealer dealer, DamageHandler dmg, CastingAbilitySO abilitySo, bool crit)
    {
        if (abilitySo is not OffensiveAbilitySO || ReferenceEquals((abilitySo as OffensiveAbilitySO).DamageBoosts, null))
        {
            return;
        }

        foreach (var item in (abilitySo as OffensiveAbilitySO).DamageBoosts)
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