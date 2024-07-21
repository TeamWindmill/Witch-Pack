using System;
using System.Collections.Generic;

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

