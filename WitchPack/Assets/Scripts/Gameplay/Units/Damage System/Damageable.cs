using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable
{
    private List<DamageDealer> _damageDealers = new();
    private IDamagable owner;
    private int currentHp;
    public int MaxHp => owner.Stats[StatType.MaxHp].IntValue;
    public int CurrentHp => currentHp;

    private bool hitable;


    Timer regenTimer;

    public event Action<int, int> OnHealthChange;
    public event Action<Damageable, DamageDealer, DamageHandler, Ability, bool> OnTakeDamage;
    public event Action<Damageable, int> OnTakeFlatDamage;
    public event Action<Damageable, int> OnHeal;
    public event Action<Damageable, DamageDealer> OnDeath;
    public event Action OnDeathGFX;
    public event Action<bool> OnHitGFX;

    public IDamagable Owner
    {
        get => owner;
    }

    public Damageable(IDamagable owner)
    {
        this.owner = owner;
        owner.Stats[StatType.MaxHp].OnStatChange += OnMaxHpChange;
    }

    public void Disable()
    {
        owner.Stats[StatType.MaxHp].OnStatChange -= OnMaxHpChange;
    }

    public void Init()
    {
        hitable = true;
        currentHp = MaxHp;
        OnHealthChange?.Invoke(currentHp, MaxHp);
    }

    public void GetHit(DamageDealer dealer, CastingAbility ability)
    {
        if (!hitable) return;

        if (ability.StatusEffects.Count > 0) owner.Effectable.AddEffects(ability.StatusEffects, dealer.Owner.Affector);

        if (ability is OffensiveAbility offensiveAbility)
        {
            DamageHandler dmg = new DamageHandler(offensiveAbility.GetAbilityStatValue(AbilityStatType.Damage));
            bool isCrit = dealer.CritChance(offensiveAbility);

            TakeDamage(dealer, dmg, offensiveAbility, isCrit);

            if (!_damageDealers.Contains(dealer))
                _damageDealers.Add(dealer);
        }
        else // in case we want to make an ability that only applys status effects
        {
            dealer.OnHitTarget?.Invoke(this, dealer, null, ability, false);
            OnHitGFX?.Invoke(false);
        }
    }

    public void TakeDamage(DamageDealer dealer, DamageHandler damage, Ability ability, bool isCrit)
    {
        if (!hitable) return;
        dealer.OnHitTarget?.Invoke(this, dealer, damage, ability, isCrit);
        OnHitGFX?.Invoke(isCrit);
        damage.ApplyArmorReduction(owner.Stats[StatType.Armor].IntValue);

        currentHp -= damage.GetDamage();
        OnTakeDamage?.Invoke(this, dealer, damage, ability, isCrit);
        OnHealthChange?.Invoke(currentHp, MaxHp);

        if (currentHp <= 0)
        {
            Die(dealer, damage, ability, isCrit);
        }

        ClampHp();
    }

    public void TakeFlatDamage(int amount)
    {
        OnHitGFX?.Invoke(false);
        currentHp -= amount;
        OnTakeFlatDamage?.Invoke(this, amount);
        OnHealthChange?.Invoke(currentHp, MaxHp);
        if (currentHp <= 0)
        {
            OnDeathGFX?.Invoke();
        }

        ClampHp();
    }

    public void Heal(int healAmount)
    {
        if (currentHp < MaxHp && healAmount > 0)
        {
            currentHp = Mathf.Clamp(currentHp + healAmount, 0, MaxHp);
            OnHeal?.Invoke(this, healAmount);
            OnHealthChange?.Invoke(currentHp, MaxHp);
        }
    }

    public void RegenHp()
    {
        Heal(owner.Stats[StatType.HpRegen].IntValue);
    }

    private void Die(DamageDealer dealer, DamageHandler damage, Ability ability, bool isCrit)
    {
        OnDeath?.Invoke(this, dealer);
        OnDeathGFX?.Invoke();
        dealer.OnKill?.Invoke(this, dealer, damage, ability, isCrit);
        damage.OnKill?.Invoke(this, damage);
        owner.ClearUnitTimers();
        foreach (var damageDealer in _damageDealers)
        {
            if (damageDealer == dealer) continue;
            damageDealer.OnAssist?.Invoke(this, dealer, damage, ability, isCrit);
        }
    }

    private void OnMaxHpChange(float newValue)
    {
        //currentHp = MaxHp;
        OnHealthChange?.Invoke(currentHp, MaxHp);
    }

    public IEnumerator TakeDamageOverTime(DamageDealer dealer, DamageHandler damage, OffensiveAbility ability, bool isCrit, float duration, float tickRate)
    {
        float elapsedTime = 0;
        float tickTimer = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += GAME_TIME.GameDeltaTime;
            tickTimer += GAME_TIME.GameDeltaTime;
            if (tickTimer >= tickRate)
            {
                tickTimer = 0;
                TakeDamage(dealer, damage, ability, isCrit);
            }

            yield return new WaitForEndOfFrame();
        }

        Debug.Log(elapsedTime);
    }

    private void ClampHp()
    {
        currentHp = Mathf.Clamp(currentHp, 0, MaxHp);
    }

    public void SetRegenerationTimer(float tickTime)
    {
        if (regenTimer != null) regenTimer.RemoveThisTimer();
        regenTimer = new Timer(new TimerData(tickTime, RegenHp, 1, true, true));
        TimerManager.AddTimer(regenTimer);
    }

    public void ToggleHitable(bool state)
    {
        hitable = state;
    }
}