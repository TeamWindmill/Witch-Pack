using System;

[Serializable]
public class Stat : BaseStat<StatType>
{
    public Stat(StatType statType, float baseValue) : base(statType, baseValue)
    {
    }
}

public enum StatType
{
    MaxHp,
    BaseDamage, // =basic attack damage
    AttackSpeed, //cdr for auto attacks 
    BaseRange, //range for all attacks 
    MovementSpeed,
    CritDamage,
    CritChance,
    InvincibleTimeNotWorking, //flat duration of invincibility after recieving damage
    AbilityCooldownReduction, //cdr for abilities only (anything that isnt an auto attack)
    Armor, // damage redcutcion from all sources
    HpRegen, //amount of health resotred every second
    BonusStatusEffectDurationNotWorking, //fixed duration added for every effect applied by unit
    AbilityProjectileSpeedNotWorking, //if an ability is projectile quicken it by this amount
    AbilityProjectilePenetrationNotWorking, //the amount of times a projectile ability can hit targets before disabling
    Invisibility,
    ThreatLevel, //the amount of threat currently on this unit
    Threat, //the amount of threat added to this unit's targets
    EnergyGainMultiplier,
    HpRegenInterval,
}

