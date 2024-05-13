public abstract class CastingAbility : Ability
{
    public CastingAbilitySO CastingConfig { get; }

    protected CastingAbility(CastingAbilitySO config, BaseUnit owner) : base(config,owner)
    {
        CastingConfig = config;
    }
    
    public abstract bool CastAbility();
    public abstract bool CheckCastAvailable();
}