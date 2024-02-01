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

    public Action OnCountReset;
    public Action OnCountIncrement;

    public int CurrentCount { get => currentCount;}

    public AbilityEventCounter(BaseUnit givenOwner, BaseAbility givenAbility, Action<Damageable, DamageDealer, DamageHandler, BaseAbility, bool> eventToSub)
    {
        owner = givenOwner;
        abilityToCount = givenAbility;
    }

    private void SameTargetAABuff(Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbility ability)
    {
        if (ReferenceEquals(ability, abilityToCount))
        {
            if (ReferenceEquals(lastTarget, target.Owner))
            {
                currentCount++;
                OnCountIncrement?.Invoke();
            }
            else
            {
                currentCount = 0;
                OnCountReset?.Invoke();
            }
        }
    }

}
