using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttritionCounter : AbilityEventCounter
{
    private BaseUnit lastTarget;
    private int maxStacks;

    public AttritionCounter(BaseUnit givenOwner, BaseAbility givenAbility, ref Action<Damageable, DamageDealer, DamageHandler, CastingAbility, bool> eventToSub, int maxStacks) : base(givenOwner, givenAbility, ref eventToSub )
    {
        this.maxStacks = maxStacks;
    }

    protected override void EventFunc(Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbility ability, bool isCrit)
    {
        if (ReferenceEquals(ability, abilityToCount))
        {
            if (ReferenceEquals(lastTarget, target.Owner)) // attacking the same target
            {
                if(currentCount < maxStacks)
                {
                    currentCount++;
                }
                OnCountIncrement?.Invoke(this, target, dealer, dmg, ability);
            }
            else // switching target
            {
                currentCount = 0;
                OnCountReset?.Invoke(this, target, dealer, dmg, ability);
                lastTarget = target.Owner as BaseUnit;
            }
        }
    }
}
