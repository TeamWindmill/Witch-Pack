using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExperiencedHunterCounter : AbilityEventCounter
{
    private int numberOfKillsRequiredToIncreasePierce;
    public ExperiencedHunterCounter(BaseUnit givenOwner, BaseAbility givenAbility, ref Action<Damageable, DamageDealer, DamageHandler, BaseAbility, bool> eventToSub, int numberOfKillsRequiredToIncreasePierce) : base(givenOwner, givenAbility, ref eventToSub)
    {
        this.numberOfKillsRequiredToIncreasePierce = numberOfKillsRequiredToIncreasePierce;        
    }

    protected override void EventFunc(Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbility ability, bool isCrit)
    {
        if (ReferenceEquals(ability, abilityToCount))
        {
            currentCount++;

            if(currentCount >= numberOfKillsRequiredToIncreasePierce)
            {
                currentCount = 0;
                OnCountIncrement?.Invoke(this, target, dealer, dmg, ability);
            }
        }
    }
}
