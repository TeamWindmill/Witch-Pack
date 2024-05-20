using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable
{
    private List<DamageDealer> _damageDealers = new List<DamageDealer>();
    private IDamagable owner;
    private int currentHp;
    public int MaxHp => owner.Stats.MaxHp;
    public int CurrentHp => currentHp;

    private bool hitable;


    Timer regenTimer;

    public event Action<Damageable, DamageDealer , DamageHandler, Ability, bool > OnGetHit;
    public event Action<int> OnTakeDamage;
    public event Action<Damageable, DamageDealer, DamageHandler, Ability, bool> OnDamageCalc;
    public event Action<Damageable, DamageDealer, DamageHandler, Ability> OnDeath;
    public event Action OnDeathGFX;
    public event Action<bool> OnHitGFX;
    public event Action<Damageable, float> OnHeal;

    //add gfx events later

    public IDamagable Owner { get => owner; }

    public Damageable(IDamagable owner)
    {
        this.owner = owner;
        hitable = true;
        currentHp = MaxHp;
        OnGetHit += AddStatsDamageReduction;
    }

    public void GetHit(DamageDealer dealer, CastingAbility ability)
    {
        if (!hitable)
        {
            return;
        }
        
        foreach (var item in ability.CastingConfig.StatusEffects)
        {
            owner.Effectable.AddEffect(item, dealer.Owner.Affector);
        }
        
        if (ability is OffensiveAbility offensiveAbility)
        {
            DamageHandler dmg = new DamageHandler(offensiveAbility.OffensiveAbilityConfig.BaseDamage);
            bool isCrit = dealer.CritChance(offensiveAbility);

            TakeDamage(dealer, dmg, offensiveAbility, isCrit);
            
            if(!_damageDealers.Contains(dealer))
                _damageDealers.Add(dealer);
        }
        else // in case we want to make an ability that only applys status effects
        {
            dealer.OnHitTarget?.Invoke(this, dealer, null, ability, false);
            OnGetHit?.Invoke(this, dealer, null, ability, false);
            OnHitGFX?.Invoke(false);
        }
    }

    public void Heal(int healAmount)
    {
        if(currentHp < MaxHp && healAmount > 0)
        {
            currentHp = Mathf.Clamp(currentHp + healAmount, 0, MaxHp);
            OnHeal?.Invoke(this, healAmount);
        }        
    }

    public void RegenHp()
    {
        Heal(owner.Stats.HpRegen);
    }

    public void TakeDamage(DamageDealer dealer, DamageHandler damage, OffensiveAbility ability, bool isCrit)
    {
        if(!hitable) return;
        dealer.OnHitTarget?.Invoke(this, dealer, damage, ability, isCrit);
        OnGetHit?.Invoke(this, dealer, damage, ability, isCrit);
        OnTakeDamage?.Invoke(damage.GetFinalDamage());
        OnHitGFX?.Invoke(isCrit);

        currentHp -= damage.GetFinalDamage();
        //Debug.Log($"{owner.gameObject} took {handler.GetFinalDamage()} damage from {dealer.Owner.name}");
        OnDamageCalc?.Invoke(this, dealer, damage, ability, isCrit);

        if (currentHp <= 0)
        {
            Die(dealer, damage, ability, isCrit);
        }
        ClampHp();
    }

    private void Die(DamageDealer dealer, DamageHandler damage, OffensiveAbility ability, bool isCrit)
    {
        OnDeath?.Invoke(this, dealer, damage, ability);
        OnDeathGFX?.Invoke();
        dealer.OnKill?.Invoke(this, dealer, damage, ability, isCrit);
        owner.ClearUnitTimers();
        foreach (var damageDealer in _damageDealers)
        {
            if (damageDealer == dealer) continue;
            damageDealer.OnAssist?.Invoke(this, dealer, damage, ability, isCrit);
        }
    }

    public IEnumerator TakeDamageOverTime(DamageDealer dealer, DamageHandler damage, OffensiveAbility ability, bool isCrit, float duration, float tickRate)
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

    public void TakeFlatDamage(int amount)  
    {
        currentHp -= amount;
        OnTakeDamage?.Invoke(amount);
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

    private void AddStatsDamageReduction(Damageable target, DamageDealer dealer, DamageHandler dmg, Ability ability, bool crit)
    {
        float damageReductionModifier = 100f / (owner.Stats.Armor + 100f);
        dmg.AddMod(damageReductionModifier);
    }

    public void DamageTick()
    {
        
    }

    public void SetRegenerationTimer()
    {
        regenTimer = new Timer(new TimerData(1, RegenHp, 1, true, true));
        TimerManager.Instance.AddTimer(regenTimer);
    }

    public void ToggleHitable(bool state)
    {
        hitable = state;
    }
}



