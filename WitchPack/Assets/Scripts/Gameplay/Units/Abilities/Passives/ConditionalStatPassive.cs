using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConditionalStatPassive", menuName = "Ability/Passive/ConditionalStat")]
public class ConditionalStatPassive : StatPassive
{
    [SerializeField] private StatusEffectConfig _conditionalStatusEffect;

    private BaseUnit _owner;
    
    public override void SubscribePassive(BaseUnit owner)
    {
        _owner = owner;
        owner.Effectable.OnAffected += TryAddStat;
        owner.Effectable.OnEffectRemoved += TryRemoveStat;
    }
    private void TryAddStat(Effectable arg1, Affector arg2, StatusEffect statusEffect)
    {
        if (statusEffect.StatusEffectType == _conditionalStatusEffect.StatusEffectType)
        {
            foreach (StatValue increase in statIncreases)
            {
                _owner.Stats.AddValueToStat(increase.StatType, increase.Value);
            }
        }
    }
    private void TryRemoveStat(StatusEffect statusEffect)
    {
        if (statusEffect.StatusEffectType == _conditionalStatusEffect.StatusEffectType)
        {
            foreach (StatValue increase in statIncreases)
            {
                _owner.Stats.AddValueToStat(increase.StatType, -increase.Value);
            }
        }
    }
}
