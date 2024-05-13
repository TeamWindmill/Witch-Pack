
public class Overheal : Heal
{
    private OverhealSO _config;
    public Overheal(OverhealSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
    }

    protected override void HealTarget(Shaman target, BaseUnit caster)
    {
        if(target.Damageable.CurrentHp + _config.HealAmount > target.Stats.MaxHp)
        { 
            target.Stats.AddValueToStat(StatType.MaxHp, _config.PermanentMaxHealthBonus);
            target.ShamanVisualHandler.OverhealEffect.Play();
        }
        else
        {
            target.ShamanVisualHandler.HealEffect.Play();
        }
        target.Damageable.Heal(_config.HealAmount);
    }
}