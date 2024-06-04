using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class Stats
{
    //base stats here - affects every unit 
    public BaseStat MaxHp = new BaseStat(StatType.MaxHp);
    public BaseStat BaseDamage = new BaseStat(StatType.BaseDamage);
    public BaseStatDecimal AttackSpeed = new BaseStatDecimal(StatType.AttackSpeed);//capped at unit stats 
    public BaseStatDecimal BaseRange = new BaseStatDecimal(StatType.BaseRange);
    public BaseStatDecimal MovementSpeed = new BaseStatDecimal(StatType.MovementSpeed);
    public BaseStat CritDamage = new BaseStat(StatType.CritDamage);
    public BaseStat CritChance = new BaseStat(StatType.CritChance);
    public BaseStat Armor = new BaseStat(StatType.Armor);
    public BaseStat HpRegen = new BaseStat(StatType.HpRegen);
    public BaseStat Threat = new BaseStat(StatType.Threat);
    public BaseStatDecimal EnergyGain = new BaseStatDecimal(StatType.EnergyGain);
    [HideInInspector] public BaseStat AbilityProjectilePenetration = new BaseStat(StatType.AbilityProjectilePenetration);
    [HideInInspector] public BaseStat Visibility = new BaseStat(StatType.Visibility);
    [HideInInspector] public BaseStat ThreatLevel = new BaseStat(StatType.ThreatLevel);

}

[Serializable]
public class BaseStat
{
    [ReadOnly] public StatType statType;
    public int value;

    public BaseStat(StatType givenStatType)
    {
        statType = givenStatType;
    }
}

[Serializable]
public class BaseStatDecimal
{
    [ReadOnly] public StatType statType;
    public float value;

    public BaseStatDecimal(StatType givenStatType)
    {
        statType = givenStatType;
    }
}