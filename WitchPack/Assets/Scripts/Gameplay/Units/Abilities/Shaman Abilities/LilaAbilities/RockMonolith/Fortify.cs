public class Fortify : RockMonolith
{
    private FortifySO _config;

    public Fortify(OffensiveAbilitySO config, BaseUnit owner) : base(config, owner)
    {
        _config = config as FortifySO;
    }

    protected override void OnTauntEnd()
    {
        base.OnTauntEnd();
        Owner.Stats[StatType.Armor].AddModifier(_config.PermanentArmorOnExplosion);
    }
}