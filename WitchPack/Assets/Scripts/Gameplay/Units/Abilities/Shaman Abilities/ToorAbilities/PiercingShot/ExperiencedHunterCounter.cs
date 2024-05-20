using System;

public class ExperiencedHunterCounter : AbilityEventCounter
{
    private int numberOfKillsRequiredToIncreasePierce;
    public ExperiencedHunterCounter(BaseUnit givenOwner, Ability ability, ref Action<Damageable, DamageDealer, DamageHandler, CastingAbility, bool> eventToSub, int numberOfKillsRequiredToIncreasePierce) : base(givenOwner, ability, ref eventToSub)
    {
        this.numberOfKillsRequiredToIncreasePierce = numberOfKillsRequiredToIncreasePierce;        
    }

    protected override void EventFunc(Damageable target, DamageDealer dealer, DamageHandler dmg, Ability ability, bool isCrit)
    {
        if (ReferenceEquals(ability, AbilityToCount))
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
