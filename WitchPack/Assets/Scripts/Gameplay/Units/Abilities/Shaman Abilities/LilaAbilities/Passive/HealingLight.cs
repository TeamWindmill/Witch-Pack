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
        _currentValue = Owner.Stats[StatType.MaxHp].Value * (GetAbilityStatValue(AbilityStatType.HpRegen) / 100);
        Owner.Stats[StatType.HpRegen].AddStatValue(Factor.Add,_currentValue);
        
        Owner.Damageable.SetRegenerationTimer(GetAbilityStatValue(AbilityStatType.TickInterval));
        
        
        //subscribe to max hp change
        Owner.Stats[StatType.MaxHp].OnStatChange += OnMaxHpChange;
    }

    private void OnMaxHpChange(float value)
    {
        Owner.Stats[StatType.HpRegen].RemoveStatValue(Factor.Add,_currentValue);
        
        _currentValue = Owner.Stats[StatType.MaxHp].Value * (GetAbilityStatValue(AbilityStatType.HpRegen) / 100);
        Owner.Stats[StatType.HpRegen].AddStatValue(Factor.Add,_currentValue);
        
    }
}