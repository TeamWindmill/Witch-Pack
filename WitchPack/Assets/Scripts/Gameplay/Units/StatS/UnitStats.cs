using System;
using UnityEngine;

[System.Serializable]
public class UnitStats
{

    public Action<StatType, float> OnStatChanged;

    private StatSheet _baseStats;

    private StatSheet ownerBaseStats => _baseStats;

    public Action OnHpRegenChange;

    public UnitStats(StatSheet baseStats)
    {
        _baseStats = baseStats;
    }

    public UnitStats()
    {

    }

    [SerializeField] private int maxHp;
    [SerializeField] private int baseDamage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int baseRange;
    [SerializeField] private float movementSpeed;
    [SerializeField] private int critDamage;
    [SerializeField] private int critChance;
    [SerializeField] private int armor;
    [SerializeField] private int hpRegen;
    [SerializeField] private int bonusStatusEffectDuration;
    [SerializeField] private int abilityProjectileSpeed;
    [SerializeField] private int abilityProjectilePenetration;
    [SerializeField] private int visibility;
    [SerializeField] private int threatLevel;

    public int MaxHp { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.MaxHp.value + maxHp), 0, (ownerBaseStats.MaxHp.value + maxHp))); } }
    public int BaseDamage { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.BaseDamage.value + baseDamage), 0, (ownerBaseStats.BaseDamage.value + baseDamage))); } }
    public float AttackSpeed { get { return Mathf.Clamp((ownerBaseStats.AttackSpeed.value + attackSpeed), 0, 2f); } }
    public int BonusRange { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.BaseRange.value + baseRange), 0, (ownerBaseStats.BaseRange.value + baseRange))); } }
    public float MovementSpeed { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.MovementSpeed.value + movementSpeed), 0, (ownerBaseStats.MovementSpeed.value + movementSpeed))); } }
    public int CritDamage { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.CritDamage.value + critDamage), 0, (ownerBaseStats.CritDamage.value + critDamage))); } }
    public int CritChance { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.CritChance.value + critChance), 0, (ownerBaseStats.CritChance.value + critChance))); } }
    public int Armor { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.Armor.value + armor), 0, (ownerBaseStats.Armor.value + armor))); } }
    public int HpRegen 
    { 
        get 
        {
            return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.HpRegen.value + hpRegen), 0, (ownerBaseStats.HpRegen.value + hpRegen))); 
        }
        private set
        {
            hpRegen = value;
            OnHpRegenChange?.Invoke();
            
        }
    }
    public int BonusStatusEffectDuration { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.BonusStatusEffectDuration.value + bonusStatusEffectDuration), 0, (ownerBaseStats.BonusStatusEffectDuration.value + bonusStatusEffectDuration))); } }
    public int AbilityProjectileSpeed { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.AbilityProjectileSpeed.value + abilityProjectileSpeed), 0, (ownerBaseStats.AbilityProjectileSpeed.value + abilityProjectileSpeed))); } }
    public int AbilityProjectilePenetration { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.AbilityProjectilePenetration.value + abilityProjectilePenetration), 0, (ownerBaseStats.AbilityProjectilePenetration.value + abilityProjectilePenetration))); } }
    public int Visibility { get { return Mathf.Clamp((ownerBaseStats.Visibility.value + visibility), 0, 1); } }
    public int ThreatLevel { get { return Mathf.Clamp((ownerBaseStats.ThreatLevel.value + threatLevel), 0, ownerBaseStats.ThreatLevel.value + threatLevel); } }

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
            case StatType.Visibility:
                return Visibility;
            case StatType.ThreatLevel:
                return ThreatLevel;
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
            case StatType.Visibility:
                return ownerBaseStats.Visibility.value;
            case StatType.ThreatLevel:
                return ownerBaseStats.ThreatLevel.value;
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
            case StatType.Armor:
                armor += wholeValue;
                break;
            case StatType.HpRegen:
                HpRegen += wholeValue;
                break;
            case StatType.BonusStatusEffectDuration:
                bonusStatusEffectDuration += wholeValue;
                break;
            case StatType.AbilityProjectilePenetration:
                abilityProjectilePenetration += wholeValue;
                break;
            case StatType.Visibility:
                visibility += wholeValue;
                break;
            case StatType.ThreatLevel:
                threatLevel += wholeValue;
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
            default:
                AddValueToStat(statType, Mathf.RoundToInt(decimalValue));
                return;
        }

        OnStatChanged?.Invoke(statType, decimalValue);
    }


}

public struct HealData
{
    float healAmount;
    BaseAbility abilityRef;

    public HealData(float healAmount, BaseAbility abilityRef)
    {
        this.healAmount = healAmount;
        this.abilityRef = abilityRef;
    }
}
