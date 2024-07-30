using System;
using System.Collections.Generic;
using Gameplay.Units.Abilities.AbilitySystem;
using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities;
using Gameplay.Units.Abilities.AbilitySystem.Casting;

namespace Gameplay.Units.Abilities
{
    public class AbilityHandler
    {
        public Action<AbilityCaster> OnCasterAdded;
        public List<AbilityCaster> CastingHandlers { get; private set; }
        public AutoAttackCaster AutoAttackCaster { get; private set; }

        protected readonly BaseUnit Owner;

        public AbilityHandler(BaseUnit owner)
        {
            Owner = owner;
            AutoAttackCaster = new AutoAttackCaster(owner, AbilityFactory.CreateAbility(owner.UnitConfig.AutoAttack,owner) as OffensiveAbility);
            CastingHandlers = new();
        }
    }
}

