using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Damageable
{
    private List<DamageDealer> _damageDealers = new List<DamageDealer>();
    private BaseUnit owner;
    private int currentHp;
    public int MaxHp => owner.Stats.MaxHp;
    public int CurrentHp => currentHp;

    private bool hitable;

    public Action<Damageable, DamageDealer /*as of this moment might be null*/, DamageHandler, BaseAbility, bool /*critical - a more generic callback*/> OnGetHit;
    public Action<Damageable, DamageDealer /*as of this moment might be null*/, DamageHandler, BaseAbility, bool> OnDamageCalc;
    public Action<Damageable, DamageDealer /*as of this moment might be null*/, DamageHandler, BaseAbility> OnDeath;
    public Action OnDeathGFX;
    public Action<bool> OnHitGFX;

    private List<DamageOverTimeData> activeDoTs;

    //add gfx events later

    public BaseUnit Owner { get => owner; }

    public Damageable(BaseUnit owner)
    {
        this.owner = owner;
        hitable = true;
        currentHp = MaxHp;
        OnGetHit += AddStatsDamageReduction;
        OnDeathGFX += DisableGo;
        activeDoTs = new List<DamageOverTimeData>();
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
            bool isCrit = dealer.CritChance(ability);

            TakeDamage(dealer, dmg, ability, isCrit);

            _damageDealers.Add(dealer);
        }
        else // in case we want to make an ability that only applys status effects
        {
            dealer.OnHitTarget?.Invoke(this, dealer, null, ability, false);
            OnGetHit?.Invoke(this, dealer, null, ability, false);
            OnHitGFX?.Invoke(false);
        }
    }

    public void TakeDamage(DamageDealer dealer, DamageHandler damage, BaseAbility ability, bool isCrit)
    {
        dealer.OnHitTarget?.Invoke(this, dealer, damage, ability, isCrit);
        OnGetHit?.Invoke(this, dealer, damage, ability, isCrit);
        OnHitGFX?.Invoke(isCrit);

        currentHp -= damage.GetFinalDamage();
        //Debug.Log($"{owner.gameObject} took {handler.GetFinalDamage()} damage from {dealer.Owner.name}");
        OnDamageCalc?.Invoke(this, dealer, damage, ability, isCrit);

        if (currentHp <= 0)
        {
            OnDeath?.Invoke(this, dealer, damage, ability);
            OnDeathGFX?.Invoke();
            dealer.OnKill?.Invoke(this, dealer, damage, ability, isCrit);
            foreach (var damageDealer in _damageDealers)
            {
                if(damageDealer == dealer) continue;
                damageDealer.OnAssist?.Invoke(this, dealer, damage, ability, isCrit);
            }
        }
        ClampHp();
    }

    public IEnumerator TakeDamageOverTime(DamageDealer dealer, DamageHandler damage, BaseAbility ability, bool isCrit, float duration, float tickRate)
    {
        float elapsedTime = 0;
        float tickTimer = 0;
        while(elapsedTime < duration)
        {
            elapsedTime += GAME_TIME.GameDeltaTime;
            tickTimer += GAME_TIME.GameDeltaTime;
            if(tickTimer >= tickRate)
            {
                tickTimer = 0;
                TakeDamage(dealer, damage, ability, isCrit);
            }
            yield return new WaitForEndOfFrame();
        }
        Debug.Log(elapsedTime);
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

    private void AddStatsDamageReduction(Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbility ability, bool crit)
    {
        dmg.AddMod(1 - (owner.Stats.Armor / 100));
    }

    public void TakeDamageOverTime(DamageDealer dealer, DamageHandler damage, BaseAbility ability, bool isCrit, int numberOfTicks, float tickTime)
    {

        DamageOverTimeData damageOverTime = new DamageOverTimeData(dealer, damage, ability, isCrit);
        activeDoTs.Add(damageOverTime);
        GAME_TIME.AddTimer(tickTime, DamageTick, true, numberOfTicks);
    }

    public void DamageTick()
    {
        
    }
}

public struct DamageOverTimeData
{
    private DamageDealer damageDealer;
    private DamageHandler damage;
    private BaseAbility ability; 
    bool isCrit;

    public DamageDealer DamageDealer { get => damageDealer;  }
    public DamageHandler Damage { get => damage;}
    public BaseAbility Ability { get => ability; }
    public bool IsCrit { get => isCrit; }

    public DamageOverTimeData(DamageDealer damageDealer, DamageHandler damage, BaseAbility ability, bool isCrit)
    {
        this.damageDealer = damageDealer;
        this.damage = damage;
        this.ability = ability;
        this.isCrit = isCrit;    
    }

}

