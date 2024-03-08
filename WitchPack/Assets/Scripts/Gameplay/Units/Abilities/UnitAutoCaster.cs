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
    [ShowInInspector] private Queue<ICaster> _queuedAbilities;
    [ShowInInspector] private List<ICaster> _abilitiesOnCooldown = new List<ICaster>();
    private float _castTimer;
    private float _currentCastTime;

    public void Init(BaseUnit givenOwner)
    {
        if(_abilitiesOnCooldown.Count > 0) _abilitiesOnCooldown.Clear();
        _queuedAbilities = new Queue<ICaster>();
        owner = givenOwner;
        foreach (var castingHandler in givenOwner.CastingHandlers)
        {
            _queuedAbilities.Enqueue(castingHandler);
        }
        _queuedAbilities.Enqueue(givenOwner.AutoAttackHandler);
        EnableCaster();
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
                    TimerManager.Instance.AddTimer(caster.GetCooldown(),caster,EnqueueAbility,true);
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
}