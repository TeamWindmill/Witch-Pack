using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAutoCaster : MonoBehaviour
{
    public bool CanCast { get; private set; }

    private BaseUnit owner;
    private Queue<ICaster> _queuedAbilities;
    private float _castTimer;
    private float _currentCastTime;

    public void Init(BaseUnit givenOwner)
    {
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
        if (_castTimer > _currentCastTime)
        {
            var caster = _queuedAbilities.Dequeue();
            if (caster.CastAbility())
            {
                TimerManager.Instance.AddTimer<ICaster>(caster.GetCooldown(),caster,EnqueueAbility,true);
                _currentCastTime = caster.Ability.CastTime;
                _castTimer = 0;
                caster.LastCast = GAME_TIME.GameDeltaTime;
            }
            else
            {
                _queuedAbilities.Enqueue(caster);
            }
        }
        else
        {
            _castTimer += GAME_TIME.GameDeltaTime;
        }
    }

    private void EnqueueAbility(ICaster caster)
    {
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
    }
}