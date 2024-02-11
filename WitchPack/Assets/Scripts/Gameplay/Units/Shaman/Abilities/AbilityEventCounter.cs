using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AbilityEventCounter 
{
    protected BaseUnit owner;
    protected BaseAbility abilityToCount;
    protected int currentCount;

    public Action<AbilityEventCounter, Damageable, DamageDealer, DamageHandler, BaseAbility> OnCountReset;
    public Action<AbilityEventCounter, Damageable, DamageDealer, DamageHandler, BaseAbility> OnCountIncrement;

    public int CurrentCount { get => currentCount;}

    public AbilityEventCounter(BaseUnit givenOwner, BaseAbility givenAbility, ref Action<Damageable, DamageDealer, DamageHandler, BaseAbility, bool> eventToSub)
    {
        owner = givenOwner;
        abilityToCount = givenAbility;
        eventToSub += EventFunc;
    }

    protected virtual void EventFunc(Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbility ability, bool isCrit)
    {
        
    }

}
