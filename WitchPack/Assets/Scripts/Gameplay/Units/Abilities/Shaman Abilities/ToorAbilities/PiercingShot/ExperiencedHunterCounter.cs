using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExperiencedHunterCounter : AbilityEventCounter
{
    private int numberOfKillsRequiredToIncreasePierce;
    public ExperiencedHunterCounter(BaseUnit givenOwner, BaseAbilitySO givenAbilitySo, ref Action<Damageable, DamageDealer, DamageHandler, CastingAbilitySO, bool> eventToSub, int numberOfKillsRequiredToIncreasePierce) : base(givenOwner, givenAbilitySo, ref eventToSub)
    {
        this.numberOfKillsRequiredToIncreasePierce = numberOfKillsRequiredToIncreasePierce;        
    }

    protected override void EventFunc(Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbilitySO abilitySo, bool isCrit)
    {
        if (ReferenceEquals(abilitySo, AbilitySoToCount))
        {
            currentCount++;

            if(currentCount >= numberOfKillsRequiredToIncreasePierce)
            {
                currentCount = 0;
                OnCountIncrement?.Invoke(this, target, dealer, dmg, abilitySo);
            }
        }
    }
}
