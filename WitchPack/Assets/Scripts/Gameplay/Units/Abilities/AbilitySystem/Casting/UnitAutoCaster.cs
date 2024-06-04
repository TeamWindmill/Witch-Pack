using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class UnitAutoCaster : MonoBehaviour
{
    public event Action<CastingAbility> CastTimeStart;
    public event Action<CastingHandsEffectType> CastTimeStartVFX;
    public event Action<CastingAbility> CastTimeEnd;
    public event Action<CastingHandsEffectType> CastTimeEndVFX;
    public bool CanCast { get; private set; }

    [ShowInInspector] private Queue<ICaster> _queuedAbilities = new();
    [ShowInInspector] private Dictionary<ICaster, Timer<ICaster>> _cooldownAbilities = new();
    private float _castTimer;
    private float _currentCastTime;
    private BaseUnit owner;

    public void Init(BaseUnit givenOwner, bool enableOnStart)
    {
        ClearData();
        owner = givenOwner;
        _castTimer = 0;
        foreach (var castingHandler in givenOwner.CastingHandlers)
        {
            _queuedAbilities.Enqueue(castingHandler);
        }

        _queuedAbilities.Enqueue(givenOwner.AutoAttackCaster);
        if (enableOnStart) EnableCaster();
    }

    private void Update()
    {
        if (!CanCast) return;
        if (_queuedAbilities.Count <= 0) return;
        var caster = _queuedAbilities.Peek();
        if (caster.CheckCastAvailable())
        {
            CastTimeStart?.Invoke(caster.Ability);
            if (caster.Ability.CastingConfig.HasCastVisual) CastTimeStartVFX?.Invoke(caster.Ability.CastingConfig.CastVisualColor);
            if (_castTimer > caster.Ability.GetAbilityStatValue(AbilityStatType.CastTime))
            {
                if (caster.CastAbility())
                {
                    CastTimeEnd?.Invoke(caster.Ability);
                    if (caster.Ability.CastingConfig.HasCastVisual) CastTimeEndVFX?.Invoke(caster.Ability.CastingConfig.CastVisualColor);
                    _cooldownAbilities.Add(caster, TimerManager.AddTimer(caster.GetCooldown(), caster, ReturnAbilityFromCooldown, true));
                    _queuedAbilities.Dequeue();
                    _castTimer = 0;
                }
            }
            else
            {
                _castTimer += GAME_TIME.GameDeltaTime;
            }
        }
        else
        {
            _castTimer = 0;
            _queuedAbilities.Dequeue();
            _queuedAbilities.Enqueue(caster);
        }
    }

    private void ReturnAbilityFromCooldown(ICaster caster)
    {
        if (!_cooldownAbilities.ContainsKey(caster)) return;
        _cooldownAbilities.Remove(caster);
        _queuedAbilities.Enqueue(caster);
    }

    public void ReplaceAbility(ICaster caster)
    {
        //if the ability is on cooldown
        foreach (var ability in _cooldownAbilities.Where(ability => ability.Key.Ability.Upgrades.Contains(caster.Ability)))
        {
            ability.Value.RemoveThisTimer();
            _cooldownAbilities.Remove(ability.Key);
            _queuedAbilities.Enqueue(caster);
            return;
        }
        //if the ability is on active queue

        foreach (var queuedAbility in _queuedAbilities)
        {
            if (queuedAbility.ContainsUpgrade(caster))
            {
                var dequeuing = true;
                while (dequeuing)
                {
                    var dequeuedCaster = _queuedAbilities.Dequeue();
                    if (dequeuedCaster.Ability.Upgrades.Contains(caster.Ability))
                    {
                        _queuedAbilities.Enqueue(caster);
                        dequeuing = false;
                    }
                    else
                    {
                        _queuedAbilities.Enqueue(dequeuedCaster);
                    }
                }

                return;
            }
        }

        _queuedAbilities.Enqueue(caster);
    }

    public void EnableCaster() => CanCast = true;


    public void DisableCaster()
    {
        CanCast = false;
        _castTimer = 0;
        if (_queuedAbilities.Count <= 0) return;
        var ability = _queuedAbilities.Peek().Ability;
        CastTimeEnd?.Invoke(ability);
        if (ability.CastingConfig.HasCastVisual) CastTimeEndVFX?.Invoke(ability.CastingConfig.CastVisualColor);
    }

    private void ClearData()
    {
        foreach (var ability in _cooldownAbilities)
        {
            ability.Value.RemoveThisTimer();
        }

        _cooldownAbilities.Clear();
        _queuedAbilities.Clear();
    }
}