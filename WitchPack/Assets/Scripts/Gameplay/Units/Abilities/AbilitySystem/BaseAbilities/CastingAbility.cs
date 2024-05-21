public abstract class CastingAbility : Ability
{
    public CastingAbilitySO CastingConfig { get; }

    protected CastingAbility(CastingAbilitySO config, BaseUnit owner) : base(config,owner)
    {
        CastingConfig = config;
        abilityStats.Add(new AbilityStat(AbilityStatType.Cooldown,config.Cd));
        abilityStats.Add(new AbilityStat(AbilityStatType.CastTime,config.CastTime));
        if(config.GivesEnergyPoints)
            abilityStats.Add(new AbilityStat(AbilityStatType.EnergyPointsOnKill,config.EnergyPoints));
    }
    
    public abstract bool CastAbility();
    public abstract bool CheckCastAvailable();
}