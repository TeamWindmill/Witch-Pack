public class BlessingOfSwiftness : Heal
{
    private BlessingOfSwiftnessSO _config;
    public BlessingOfSwiftness(BlessingOfSwiftnessSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
    }

    protected override void HealTarget(Shaman target, BaseUnit caster)
    {
        target.Damageable.Heal(_config.HealAmount);
        target.ShamanVisualHandler.HealEffect.Play();
        target.Effectable.AddEffect(_config.AttackSpeedBoost, caster.Affector);
    }
}