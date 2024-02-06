using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttritionCounter : AbilityEventCounter
{
    private BaseUnit lastTarget;

    public AttritionCounter(BaseUnit givenOwner, BaseAbility givenAbility, ref Action<Damageable, DamageDealer, DamageHandler, BaseAbility, bool> eventToSub) : base(givenOwner, givenAbility, ref eventToSub )
    {
        owner = givenOwner;
        abilityToCount = givenAbility;
        eventToSub += EventFunc;
    }

    protected override void EventFunc(Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbility ability, bool isCrit)
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
