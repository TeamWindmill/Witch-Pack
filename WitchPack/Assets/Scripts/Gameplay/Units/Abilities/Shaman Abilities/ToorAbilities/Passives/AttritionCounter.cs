using System;

public class AttritionCounter : AbilityEventCounter
{
    private BaseUnit lastTarget;
    private int maxStacks;

    public AttritionCounter(BaseUnit givenOwner, Ability ability, ref Action<Damageable, DamageDealer, DamageHandler, Ability, bool> eventToSub, int maxStacks) : base(givenOwner, ability, ref eventToSub )
    {
        this.maxStacks = maxStacks;
    }

    protected override void EventFunc(Damageable target, DamageDealer dealer, DamageHandler dmg, Ability ability, bool isCrit)
    {
        if (ReferenceEquals(ability, AbilityToCount))
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
