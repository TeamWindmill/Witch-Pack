using System;
public class AbilityEventCounter 
{
    protected BaseUnit owner;
    protected AbilitySO AbilitySoToCount;
    protected int currentCount;

    public Action<AbilityEventCounter, Damageable, DamageDealer, DamageHandler, AbilitySO> OnCountReset;
    public Action<AbilityEventCounter, Damageable, DamageDealer, DamageHandler, AbilitySO> OnCountIncrement;

    public int CurrentCount { get => currentCount;}

    public AbilityEventCounter(BaseUnit givenOwner, AbilitySO givenAbilitySo, ref Action<Damageable, DamageDealer, DamageHandler, CastingAbilitySO, bool> eventToSub)
    {
        owner = givenOwner;
        AbilitySoToCount = givenAbilitySo;
        eventToSub += EventFunc;
    }

    protected virtual void EventFunc(Damageable target, DamageDealer dealer, DamageHandler dmg, AbilitySO abilitySo, bool isCrit)
    {
        
    }

}
