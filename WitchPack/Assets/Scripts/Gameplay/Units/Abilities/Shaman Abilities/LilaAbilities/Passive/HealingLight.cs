public class HealingLight : StatPassive
{
    private HealingLightSO _config;
    private float _currentValue;

    public HealingLight(StatPassiveSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config as HealingLightSO;
        abilityStats.Add(new AbilityStat(AbilityStatType.HpRegen, _config.HealPercentage));
        abilityStats.Add(new AbilityStat(AbilityStatType.TickInterval, _config.HealInterval));
    }

    public override void SubscribePassive()
    {
        TimerManager.AddTimer(GetAbilityStatValue(AbilityStatType.TickInterval), RegenHp, true, dontDestroyTimer: true);
    }

    private void RegenHp()
    {
        _currentValue = Owner.Stats[StatType.MaxHp].Value * (GetAbilityStatValue(AbilityStatType.HpRegen) / 100);
        Owner.Damageable.Heal((int)_currentValue);
    }
}