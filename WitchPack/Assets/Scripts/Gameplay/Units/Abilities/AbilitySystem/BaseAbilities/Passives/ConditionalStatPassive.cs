public class ConditionalStatPassive : StatPassive
{
    private ConditionalStatPassiveSO _config;
    public ConditionalStatPassive(ConditionalStatPassiveSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
    }
    
    public override void SubscribePassive()
    {
        Owner.Effectable.OnAffected += TryAddStat;
        Owner.Effectable.OnEffectRemoved += TryRemoveStat;
    }
    
    private void TryAddStat(Effectable arg1, Affector arg2, StatusEffect statusEffect)
    {
        if (statusEffect.StatusEffectType == _config.ConditionalStatusEffect.StatusEffectType)
        {
            foreach (StatValue increase in _config.StatIncreases)
            {
                Owner.Stats.AddModifierToStat(increase.StatType, increase.Value);
            }
        }
    }
    private void TryRemoveStat(StatusEffect statusEffect)
    {
        if (statusEffect.StatusEffectType == _config.ConditionalStatusEffect.StatusEffectType)
        {
            foreach (StatValue increase in _config.StatIncreases)
            {
                Owner.Stats.AddModifierToStat(increase.StatType, -increase.Value);
            }
        }
    }
}