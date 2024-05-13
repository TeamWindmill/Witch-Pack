using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttritionCounter : AbilityEventCounter
{
    private BaseUnit lastTarget;
    private int maxStacks;

    public AttritionCounter(BaseUnit givenOwner, BaseAbilitySO givenAbilitySo, ref Action<Damageable, DamageDealer, DamageHandler, CastingAbilitySO, bool> eventToSub, int maxStacks) : base(givenOwner, givenAbilitySo, ref eventToSub )
    {
        this.maxStacks = maxStacks;
    }

    protected override void EventFunc(Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbilitySO abilitySo, bool isCrit)
    {
        if (ReferenceEquals(abilitySo, AbilitySoToCount))
        {
            if (ReferenceEquals(lastTarget, target.Owner)) // attacking the same target
            {
                if(currentCount < maxStacks)
                {
                    currentCount++;
                }
                OnCountIncrement?.Invoke(this, target, dealer, dmg, abilitySo);
            }
            else // switching target
            {
                currentCount = 0;
                OnCountReset?.Invoke(this, target, dealer, dmg, abilitySo);
                lastTarget = target.Owner as BaseUnit;
            }
        }
    }
}
