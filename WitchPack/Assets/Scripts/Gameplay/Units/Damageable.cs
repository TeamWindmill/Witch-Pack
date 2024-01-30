using System;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class Damageable
{
    private BaseUnit owner;
    private int currentHp;
    public int MaxHp => owner.Stats.MaxHp;
    public int CurrentHp => currentHp;

    private bool hitable;

    public Action<Damageable, DamageDealer /*as of this moment might be null*/, DamageHandler, BaseAbility, bool /*critical - a more generic callback*/> OnGetHit;
    public Action<Damageable, DamageDealer /*as of this moment might be null*/, DamageHandler, BaseAbility> OnDamageCalc;
    public Action<Damageable, DamageDealer /*as of this moment might be null*/, DamageHandler, BaseAbility> OnDeath;
    public Action OnDeathGFX;
    public Action<bool> OnHitGFX;
    
    //add gfx events later

    public BaseUnit Owner { get => owner; }

    public Damageable(BaseUnit owner)
    {
        this.owner = owner;
        hitable = true;
        currentHp = MaxHp;
        OnGetHit += AddStatsDamageReduction;
        OnDeathGFX += DisableGo;
    }

    private void DisableGo()
    {
        //Owner.gameObject.SetActive(false);
    }

    public void GetHit(DamageDealer dealer, BaseAbility ability)
    {
        if (!hitable)
        {
            return;
        }
        //status effects addition
        foreach (var item in ability.StatusEffects)
        {
            owner.Effectable.AddEffect(item, dealer.Owner.Affector);
        }

        if (ability is OffensiveAbility)
        {
            DamageHandler dmg = new DamageHandler((ability as OffensiveAbility).BaseDamage);
            if (dealer.CritChance(ability))
            {
                dealer.OnHitTarget?.Invoke(this, dealer, dmg, ability, true);
                OnGetHit?.Invoke(this, dealer, dmg, ability, true);
                OnHitGFX?.Invoke(true);
            }
            else
            {
                dealer.OnHitTarget?.Invoke(this, dealer, dmg, ability, false);
                OnGetHit?.Invoke(this, dealer, dmg, ability, false);
                OnHitGFX?.Invoke(false);
            }
            TakeDamage(dmg, dealer, ability);
        }
        else // in case we want to make an ability that only applys status effects
        {
            dealer.OnHitTarget?.Invoke(this, dealer, null, ability, false);
            OnGetHit?.Invoke(this, dealer, null, ability, false);
            OnHitGFX?.Invoke(false);
        }
    }

    public void TakeDamage(DamageHandler handler, DamageDealer dealer, BaseAbility attack)
    {
        currentHp -= handler.GetFinalDamage();
        //Debug.Log($"{owner.gameObject} took {handler.GetFinalDamage()} damage from {dealer.Owner.name}");
        OnDamageCalc?.Invoke(this,dealer,handler,attack);
        if (currentHp <= 0)
        {
            OnDeath?.Invoke(this, dealer, handler, attack);
            dealer.OnKill?.Invoke(this, dealer, handler, attack);
            OnDeathGFX?.Invoke();
        }
        ClampHp();
    }

    public void TakeFlatDamage(int amount)  //DOES NOT TRIGGER EVENTS!
    {
        currentHp -= amount;
        if (currentHp <= 0)
        {
            OnDeathGFX?.Invoke();
        }
        ClampHp();
    }

    private void ClampHp()
    {
        currentHp = Mathf.Clamp(currentHp, 0, MaxHp);
    }

    private void AddStatsDamageReduction(Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbility ability, bool crit )
    {
        dmg.AddMod(1 - (owner.Stats.Armor / 100));
    }
}
