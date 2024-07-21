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
        if (statusEffect.StatusEffectVisual == _config.ConditionalStatusEffect.StatusEffectVisual)
        {
            foreach (var statIncrease in PassiveAbilityStats)
            {
                Owner.Stats[statIncrease.StatType].AddModifier(statIncrease.Value);
            }
        }
    }
    private void TryRemoveStat(StatusEffect statusEffect)
    {
        if (statusEffect.StatusEffectVisual == _config.ConditionalStatusEffect.StatusEffectVisual)
        {
            foreach (var statIncrease in PassiveAbilityStats)
            {
                Owner.Stats[statIncrease.StatType].RemoveModifier(statIncrease.Value);
            }
        }
    }
}