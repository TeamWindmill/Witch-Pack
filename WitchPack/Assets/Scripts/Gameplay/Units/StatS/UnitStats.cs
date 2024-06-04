using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitStats
{
    public event Action<StatType, float> OnStatChanged;

    private Stats _baseStats;

    public Stats OwnerBaseStats => _baseStats;

    public Action OnHpRegenChange;

    public Dictionary<StatType, Stat> Stats = new();

    public UnitStats(Stats baseStats)
    {
        _baseStats = baseStats;

        Stats.Add(baseStats.MaxHp.statType,new Stat(baseStats.MaxHp.statType,baseStats.MaxHp.value));
        Stats.Add(baseStats.BaseDamage.statType,new Stat(baseStats.BaseDamage.statType,baseStats.BaseDamage.value));
        Stats.Add(baseStats.AttackSpeed.statType,new Stat(baseStats.AttackSpeed.statType,baseStats.AttackSpeed.value));
        Stats.Add(baseStats.BaseRange.statType,new Stat(baseStats.BaseRange.statType,baseStats.BaseRange.value));
        Stats.Add(baseStats.MovementSpeed.statType,new Stat(baseStats.MovementSpeed.statType,baseStats.MovementSpeed.value));
        Stats.Add(baseStats.CritDamage.statType,new Stat(baseStats.CritDamage.statType,baseStats.CritDamage.value));
        Stats.Add(baseStats.CritChance.statType,new Stat(baseStats.CritChance.statType,baseStats.CritChance.value));
        Stats.Add(baseStats.Armor.statType,new Stat(baseStats.Armor.statType,baseStats.Armor.value));
        Stats.Add(baseStats.HpRegen.statType,new Stat(baseStats.HpRegen.statType,baseStats.HpRegen.value));
        Stats.Add(baseStats.Threat.statType,new Stat(baseStats.Threat.statType,baseStats.Threat.value));
        Stats.Add(baseStats.AbilityProjectilePenetration.statType,new Stat(baseStats.AbilityProjectilePenetration.statType,baseStats.AbilityProjectilePenetration.value));
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

    public void AddValueToStat(StatType statType, float decimalValue) //can be used to reduce or increase
    {
        Stats[statType].AddModifier(decimalValue);
    }
    
    public void AddMultiplierToStat(StatType statType, float decimalValue)
    {
        Stats[statType].AddMultiplier(decimalValue);
    }
}

public struct HealData
{
    float healAmount;
    AbilitySO abilitySoRef;

    public HealData(float healAmount, AbilitySO abilitySoRef)
    {
        this.healAmount = healAmount;
        this.abilitySoRef = abilitySoRef;
    }
}