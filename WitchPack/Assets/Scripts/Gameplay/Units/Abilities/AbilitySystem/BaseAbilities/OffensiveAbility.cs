public abstract class OffensiveAbility : CastingAbility
{
    public OffensiveAbilitySO OffensiveAbilityConfig { get; }

    protected OffensiveAbility(OffensiveAbilitySO config, BaseUnit owner) : base(config, owner)
    {
        OffensiveAbilityConfig = config;
    }
}