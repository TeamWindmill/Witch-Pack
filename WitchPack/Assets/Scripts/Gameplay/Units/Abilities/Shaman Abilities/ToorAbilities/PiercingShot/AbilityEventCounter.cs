using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AbilityEventCounter 
{
    protected BaseUnit owner;
    protected BaseAbilitySO AbilitySoToCount;
    protected int currentCount;

    public Action<AbilityEventCounter, Damageable, DamageDealer, DamageHandler, BaseAbilitySO> OnCountReset;
    public Action<AbilityEventCounter, Damageable, DamageDealer, DamageHandler, BaseAbilitySO> OnCountIncrement;

    public int CurrentCount { get => currentCount;}

    public AbilityEventCounter(BaseUnit givenOwner, BaseAbilitySO givenAbilitySo, ref Action<Damageable, DamageDealer, DamageHandler, CastingAbilitySO, bool> eventToSub)
    {
        owner = givenOwner;
        AbilitySoToCount = givenAbilitySo;
        eventToSub += EventFunc;
    }

    protected virtual void EventFunc(Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbilitySO abilitySo, bool isCrit)
    {
        
    }

}
