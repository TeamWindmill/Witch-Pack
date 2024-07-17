using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[System.Serializable]
public class UnitStats
{

    private Stats _baseStats;

    public Stats OwnerBaseStats => _baseStats;

    public Action OnHpRegenChange;

    public Dictionary<StatType, Stat> Stats = new();
    public UnitStats(Stats baseStats)
    {
        _baseStats = baseStats;

        Stats.Add(baseStats.MaxHp.statType,new Stat(baseStats.MaxHp.statType,baseStats.MaxHp.value));
        Stats.Add(baseStats._damage.statType,new Stat(baseStats._damage.statType,baseStats._damage.value));
        Stats.Add(baseStats.AttackSpeed.statType,new Stat(baseStats.AttackSpeed.statType,baseStats.AttackSpeed.value));
        Stats.Add(baseStats._range.statType,new Stat(baseStats._range.statType,baseStats._range.value));
        Stats.Add(baseStats.MovementSpeed.statType,new Stat(baseStats.MovementSpeed.statType,baseStats.MovementSpeed.value));
        Stats.Add(baseStats.CritDamage.statType,new Stat(baseStats.CritDamage.statType,baseStats.CritDamage.value));
        Stats.Add(baseStats.CritChance.statType,new Stat(baseStats.CritChance.statType,baseStats.CritChance.value));
        Stats.Add(baseStats.Armor.statType,new Stat(baseStats.Armor.statType,baseStats.Armor.value));
        Stats.Add(baseStats.HpRegen.statType,new Stat(baseStats.HpRegen.statType,baseStats.HpRegen.value));
        Stats.Add(baseStats.HpRegenInterval.statType,new Stat(baseStats.HpRegenInterval.statType,baseStats.HpRegenInterval.value));
        Stats.Add(baseStats.Threat.statType,new Stat(baseStats.Threat.statType,baseStats.Threat.value));
        Stats.Add(baseStats.AbilityCooldownReduction.statType,new Stat(baseStats.AbilityCooldownReduction.statType,baseStats.AbilityCooldownReduction.value));
        Stats.Add(baseStats.Visibility.statType,new Stat(baseStats.Visibility.statType,baseStats.Visibility.value));
        Stats.Add(baseStats.ThreatLevel.statType,new Stat(baseStats.ThreatLevel.statType,baseStats.ThreatLevel.value));
        Stats.Add(baseStats.EnergyGain.statType,new Stat(baseStats.EnergyGain.statType,baseStats.EnergyGain.value));
    }

    public Stat this[StatType statType]
    {
        get => Stats[statType];
    }
    
    public float GetStatValue(StatType statTypeId)
    {
        return Stats[statTypeId].Value;
    }

    public float GetBaseStatValue(StatType statTypeId)
    {
        return Stats[statTypeId].BaseValue;
    }

    public void AddValueToStat(StatType statType, Factor factor, float decimalValue)
    {
        Stats[statType].AddStatValue(factor,decimalValue);
    }
    public void RemoveValueFromStat(StatType statType, Factor factor, float decimalValue)
    {
        Stats[statType].RemoveStatValue(factor,decimalValue);
    }
    public void AddModifierToStat(StatType statType, float decimalValue)
    {
        Stats[statType].AddModifier(decimalValue);
    }
    
    public void AddMultiplierToStat(StatType statType, float decimalValue)
    {
        Stats[statType].AddMultiplier(decimalValue);
    }
}