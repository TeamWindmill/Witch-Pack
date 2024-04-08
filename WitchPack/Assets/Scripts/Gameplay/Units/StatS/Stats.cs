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
    [HideInInspector] public BaseStat AbilityProjectilePenetration = new BaseStat(StatType.AbilityProjectilePenetration);
    [HideInInspector] public BaseStat Visibility = new BaseStat(StatType.Visibility);
    [HideInInspector] public BaseStat ThreatLevel = new BaseStat(StatType.ThreatLevel);

}

public enum StatType
{
    MaxHp,
    BaseDamage,// =basic attack damage
    AttackSpeed,//cdr for auto attacks 
    BaseRange,//range for all attacks 
    MovementSpeed,
    CritDamage,
    CritChance,
    InvincibleTime,//flat duration of invincibility after recieving damage
    AbilityCooldownReduction,//cdr for abilities only (anything that isnt an auto attack)
    Armor,// damage redcutcion from all sources
    HpRegen,//amount of health resotred every second
    BonusStatusEffectDuration,//fixed duration added for every effect applied by unit
    AbilityProjectileSpeed,//if an ability is projectile quicken it by this amount
    AbilityProjectilePenetration,//the amount of times a projectile ability can hit targets before disabling
    Visibility,
    ThreatLevel,
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