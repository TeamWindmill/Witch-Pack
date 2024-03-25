using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

public class UnitAutoCaster : MonoBehaviour
{
    public event Action<CastingAbility> CastTimeStart;
    public event Action<CastingHandsEffectType> CastTimeStartVFX;
    public event Action<CastingAbility> CastTimeEnd;
    public event Action<CastingHandsEffectType> CastTimeEndVFX;
    public bool CanCast { get; private set; }

    private BaseUnit owner;
    [ShowInInspector] private Queue<ICaster> _queuedAbilities = new();
    [ShowInInspector] private List<ICaster> _abilitiesOnCooldown = new();
    [ShowInInspector] private List<Timer<ICaster>> _activeTimers = new();
    private float _castTimer;
    private float _currentCastTime;

    public void Init(BaseUnit givenOwner,bool enableOnStart)
    {
        ClearData();
        owner = givenOwner;
        _castTimer = 0;
        foreach (var castingHandler in givenOwner.CastingHandlers)
        {
            _queuedAbilities.Enqueue(castingHandler);
        }
        _queuedAbilities.Enqueue(givenOwner.AutoAttackHandler);
        if(enableOnStart) EnableCaster();
    }

    private void Update()
    {
        if(!CanCast) return;
        if (_queuedAbilities.Count <= 0) return;
        var caster = _queuedAbilities.Peek();
        if (caster.CheckCastAvailable())
        {
            CastTimeStart?.Invoke(caster.Ability);
            if(caster.Ability.HasCastVisual) CastTimeStartVFX?.Invoke(caster.Ability.CastVisualColor);
            if (_castTimer > caster.Ability.CastTime)
            {
                if (caster.CastAbility())
                {
                    CastTimeEnd?.Invoke(caster.Ability);
                    if(caster.Ability.HasCastVisual) CastTimeEndVFX?.Invoke(caster.Ability.CastVisualColor);
                    _activeTimers.Add(TimerManager.Instance.AddTimer(caster.GetCooldown(),caster,EnqueueAbility,true));
                    _abilitiesOnCooldown.Add(caster);
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

    private void EnqueueAbility(ICaster caster)
    {
        if (!_abilitiesOnCooldown.Contains(caster)) return;
        _abilitiesOnCooldown.Remove(caster);
        _queuedAbilities.Enqueue(caster);
    }

    public void EnableCaster()
    {
        CanCast = true;
    }
    public void DisableCaster()
    {
        CanCast = false;
        _castTimer = 0;
        if(_queuedAbilities.Count <= 0) return;
        var ability = _queuedAbilities.Peek().Ability;
        CastTimeEnd?.Invoke(ability);
        if(ability.HasCastVisual) CastTimeEndVFX?.Invoke(ability.CastVisualColor);
    }
    private void ClearData()
    {
        if(_abilitiesOnCooldown.Count > 0) _abilitiesOnCooldown.Clear();
        if(_activeTimers.Count > 0)
        {
            foreach (var timer in _activeTimers)
            {
                timer.RemoveThisTimer();
            }
            _activeTimers.Clear();
        }
        _queuedAbilities.Clear();
    }
}