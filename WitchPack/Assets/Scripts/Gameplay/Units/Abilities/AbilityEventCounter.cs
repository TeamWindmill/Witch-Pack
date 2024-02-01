using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AbilityEventCounter 
{
    private BaseUnit owner;
    private BaseAbility abilityToCount;
    private BaseUnit lastTarget;
    private int currentCount;

    public Action<AbilityEventCounter, Damageable, DamageDealer, DamageHandler, BaseAbility> OnCountReset;
    public Action<AbilityEventCounter, Damageable, DamageDealer, DamageHandler, BaseAbility> OnCountIncrement;

    public int CurrentCount { get => currentCount;}

    public AbilityEventCounter(BaseUnit givenOwner, BaseAbility givenAbility, ref Action<Damageable, DamageDealer, DamageHandler, BaseAbility, bool> eventToSub)
    {
        owner = givenOwner;
        abilityToCount = givenAbility;
        eventToSub += SameTargetCounter;
    }

    private void SameTargetCounter(Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbility ability, bool isCrit)
    {
        if (ReferenceEquals(ability, abilityToCount))
        {
            if (ReferenceEquals(lastTarget, target.Owner))
            {
                currentCount++;
                OnCountIncrement?.Invoke(this, target, dealer, dmg, ability);
            }
            else
            {
                currentCount = 0;
                OnCountReset?.Invoke(this, target, dealer, dmg, ability);
                lastTarget = target.Owner;
            }
        }
    }

}
