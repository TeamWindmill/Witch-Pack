using System;
using UnityEngine;

[System.Serializable]
public class UnitStats
{

    public Action<StatType, float> OnStatChanged;

    private StatSheet _baseStats;

    private StatSheet ownerBaseStats => _baseStats;

    public UnitStats(StatSheet baseStats)
    {
        _baseStats = baseStats;
    }

    public UnitStats()
    {

    }

    private int maxHp;
    private int baseDamage;
    private float attackSpeed;
    private int baseRange;
    private float movementSpeed;
    private int critDamage;
    private int critChance;
    private float invincibleTime;
    private int abilityCooldownReduction;
    private int armor;
    private int hpRegen;
    private int bonusStatusEffectDuration;
    private int abilityProjectileSpeed;
    private int abilityProjectilePenetration;
    private float visibility;

    public int MaxHp { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.MaxHp.value + maxHp), 0, (ownerBaseStats.MaxHp.value + maxHp))); } }
    public int BaseDamage { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.BaseDamage.value + baseDamage), 0, (ownerBaseStats.BaseDamage.value + baseDamage))); } }
    public float AttackSpeed { get { return Mathf.Clamp((ownerBaseStats.AttackSpeed.value + attackSpeed), 0, 2f); } }
    public int BonusRange { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.BaseRange.value + baseRange), 0, (ownerBaseStats.BaseRange.value + baseRange))); } }
    public float MovementSpeed { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.MovementSpeed.value + movementSpeed), 0, (ownerBaseStats.MovementSpeed.value + movementSpeed))); } }
    public int CritDamage { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.CritDamage.value + critDamage), 0, (ownerBaseStats.CritDamage.value + critDamage))); } }
    public int CritChance { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.CritChance.value + critChance), 0, (ownerBaseStats.CritChance.value + critChance))); } }
    public float InvincibleTime { get { return Mathf.Clamp((ownerBaseStats.InvincibleTime.value + invincibleTime), 0, 0.5f); } }
    public int AbilityCooldownReduction { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.AbilityCooldownReduction.value + abilityCooldownReduction), 0, (ownerBaseStats.AbilityCooldownReduction.value + abilityCooldownReduction))); } }
    public int Armor { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.Armor.value + armor), 0, (ownerBaseStats.Armor.value + armor))); } }
    public int HpRegen { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.HpRegen.value + hpRegen), 0, (ownerBaseStats.HpRegen.value + hpRegen))); } }
    public int BonusStatusEffectDuration { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.BonusStatusEffectDuration.value + bonusStatusEffectDuration), 0, (ownerBaseStats.BonusStatusEffectDuration.value + bonusStatusEffectDuration))); } }
    public int AbilityProjectileSpeed { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.AbilityProjectileSpeed.value + abilityProjectileSpeed), 0, (ownerBaseStats.AbilityProjectileSpeed.value + abilityProjectileSpeed))); } }
    public int AbilityProjectilePenetration { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.AbilityProjectilePenetration.value + abilityProjectilePenetration), 0, (ownerBaseStats.AbilityProjectilePenetration.value + abilityProjectilePenetration))); } }
    public float Visibility { get { return Mathf.Clamp((ownerBaseStats.Visibility.value + visibility), 0, 1); } }

    public float GetStatValue(StatType statTypeId)
    {
        switch (statTypeId)
        {
            case StatType.MaxHp:
                return MaxHp;
            case StatType.BaseDamage:
                return BaseDamage;
            case StatType.AttackSpeed:
                return AttackSpeed;
            case StatType.BaseRange:
                return BonusRange;
            case StatType.MovementSpeed:
                return MovementSpeed;
            case StatType.CritDamage:
                return CritDamage;
            case StatType.CritChance:
                return CritChance;
            case StatType.InvincibleTime:
                return InvincibleTime;
            case StatType.AbilityCooldownReduction:
                return AbilityCooldownReduction;
            case StatType.Armor:
                return Armor;
            case StatType.HpRegen:
                return HpRegen;
            case StatType.BonusStatusEffectDuration:
                return BonusStatusEffectDuration;
            case StatType.AbilityProjectileSpeed:
                return AbilityProjectileSpeed;
            case StatType.AbilityProjectilePenetration:
                return AbilityProjectilePenetration;
            default:
                return 0;
        }
    }
    public float GetBaseStatValue(StatType statTypeId)
    {
        switch (statTypeId)
        {
            case StatType.MaxHp:
                return ownerBaseStats.MaxHp.value;
            case StatType.BaseDamage:
                return ownerBaseStats.BaseDamage.value;
            case StatType.AttackSpeed:
                return ownerBaseStats.AttackSpeed.value;
            case StatType.BaseRange:
                return ownerBaseStats.BaseRange.value;
            case StatType.MovementSpeed:
                return ownerBaseStats.MovementSpeed.value;
            case StatType.CritDamage:
                return ownerBaseStats.CritDamage.value;
            case StatType.CritChance:
                return ownerBaseStats.CritChance.value;
            case StatType.InvincibleTime:
                return ownerBaseStats.InvincibleTime.value;
            case StatType.AbilityCooldownReduction:
                return ownerBaseStats.AbilityCooldownReduction.value;
            case StatType.Armor:
                return ownerBaseStats.Armor.value;
            case StatType.HpRegen:
                return ownerBaseStats.HpRegen.value;
            case StatType.BonusStatusEffectDuration:
                return ownerBaseStats.BonusStatusEffectDuration.value;
            case StatType.AbilityProjectileSpeed:
                return ownerBaseStats.AbilityProjectileSpeed.value;
            case StatType.AbilityProjectilePenetration:
                return ownerBaseStats.AbilityProjectilePenetration.value;
            default:
                return 0;
        }
    }
    public void AddValueToStat(StatType statType, int wholeValue) //can be used to reduce or increase
    {
        switch (statType)
        {
            case StatType.MaxHp:
                maxHp += wholeValue;
                break;
            case StatType.BaseDamage:
                baseDamage += wholeValue;
                break;
            case StatType.AttackSpeed:
                attackSpeed += wholeValue;
                break;
            case StatType.BaseRange:
                baseRange += wholeValue;
                break;
            case StatType.MovementSpeed:
                movementSpeed += wholeValue;
                break;
            case StatType.CritDamage:
                critDamage += wholeValue;
                break;
            case StatType.CritChance:
                critChance += wholeValue;
                break;
            case StatType.InvincibleTime:
                invincibleTime += wholeValue;
                break;
            case StatType.AbilityCooldownReduction:
                abilityCooldownReduction += wholeValue;
                break;
            case StatType.Armor:
                armor += wholeValue;
                break;
            case StatType.HpRegen:
                hpRegen += wholeValue;
                break;
            case StatType.BonusStatusEffectDuration:
                bonusStatusEffectDuration += wholeValue;
                break;
            case StatType.AbilityProjectilePenetration:
                abilityProjectilePenetration += wholeValue;
                break;
            
            default:
                break;
        }

        OnStatChanged?.Invoke(statType, wholeValue);
    }

    public void AddValueToStat(StatType statType, float decimalValue) //can be used to reduce or increase
    {
        switch (statType)
        {
            case StatType.AttackSpeed:
                attackSpeed += decimalValue;
                break;
            case StatType.MovementSpeed:
                movementSpeed += decimalValue;
                break;
            case StatType.InvincibleTime:
                invincibleTime += decimalValue;
                break;
            case StatType.Visibility:
                visibility += decimalValue;
                break;
            default:
                AddValueToStat(statType, Mathf.RoundToInt(decimalValue));
                break;
        }

        OnStatChanged?.Invoke(statType, decimalValue);
    }


}
