using System;
using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities;
using Gameplay.Units.Damage_System;

namespace Gameplay.Units.Abilities.Shaman_Abilities.ToorAbilities.PiercingShot
{
    public class AbilityEventCounter 
    {
        protected BaseUnit owner;
        protected Ability AbilityToCount;
        protected int currentCount;

        public Action<AbilityEventCounter, Damageable, DamageDealer, DamageHandler, Ability> OnCountReset;
        public Action<AbilityEventCounter, Damageable, DamageDealer, DamageHandler, Ability> OnCountIncrement;

        public int CurrentCount { get => currentCount;}

        public AbilityEventCounter(BaseUnit givenOwner, Ability ability, ref Action<Damageable, DamageDealer, DamageHandler, Ability, bool> eventToSub)
        {
            owner = givenOwner;
            AbilityToCount = ability;
            eventToSub += EventFunc;
        }

        protected virtual void EventFunc(Damageable target, DamageDealer dealer, DamageHandler dmg, Ability ability, bool isCrit)
        {
        
        }

    }
}
