using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class Stats
{
    //base stats here - affects every unit 
    public StatConfig MaxHp = new StatConfig(StatType.MaxHp);
    public StatConfig _damage = new StatConfig(StatType.BaseDamage);
    public StatDecimalConfig AttackSpeed = new StatDecimalConfig(StatType.AttackSpeed);//capped at unit stats 
    public StatDecimalConfig _range = new StatDecimalConfig(StatType.BaseRange);
    public StatDecimalConfig MovementSpeed = new StatDecimalConfig(StatType.MovementSpeed);
    public StatConfig CritDamage = new StatConfig(StatType.CritDamage);
    public StatConfig CritChance = new StatConfig(StatType.CritChance);
    public StatConfig Armor = new StatConfig(StatType.Armor);
    public StatConfig HpRegen = new StatConfig(StatType.HpRegen);
    public StatConfig Threat = new StatConfig(StatType.Threat);
    public StatDecimalConfig EnergyGain = new StatDecimalConfig(StatType.EnergyGain);
    [HideInInspector] public StatConfig AbilityProjectilePenetration = new StatConfig(StatType.AbilityProjectilePenetration);
    [HideInInspector] public StatConfig Visibility = new StatConfig(StatType.Visibility);
    [HideInInspector] public StatConfig ThreatLevel = new StatConfig(StatType.ThreatLevel);

}

[Serializable]
public class StatConfig
{
    [ReadOnly] public StatType statType;
    public int value;

    public StatConfig(StatType givenStatType)
    {
        statType = givenStatType;
    }
}

[Serializable]
public class StatDecimalConfig
{
    [ReadOnly] public StatType statType;
    public float value;

    public StatDecimalConfig(StatType givenStatType)
    {
        statType = givenStatType;
    }
}