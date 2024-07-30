using System;
using Gameplay.Units.Abilities.AbilitySystem.AbilityStats;
using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities;
using Gameplay.Units.Damage_System;

namespace Gameplay.Units.Abilities.Shaman_Abilities.ToorAbilities.PiercingShot
{
    public class ExperiencedHunterCounter : AbilityEventCounter
    {
        private Ability _ability;
        public ExperiencedHunterCounter(BaseUnit givenOwner, Ability ability, ref Action<Damageable, DamageDealer, DamageHandler, Ability, bool> eventToSub) : base(givenOwner, ability, ref eventToSub)
        {
            _ability = ability;
        }

        protected override void EventFunc(Damageable target, DamageDealer dealer, DamageHandler dmg, Ability ability, bool isCrit)
        {
            if (ReferenceEquals(ability, AbilityToCount))
            {
                currentCount++;

                if(currentCount >= _ability.GetAbilityStatValue(AbilityStatType.KillToIncreasePenetration))
                {
                    currentCount = 0;
                    OnCountIncrement?.Invoke(this, target, dealer, dmg, ability);
                }
            }
        }
    }
}
