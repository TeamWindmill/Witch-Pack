using System;
using UnityEngine;

[System.Serializable]
public class UnitStats
{

    public event Action<StatType, float> OnStatChanged;

    private Stats _baseStats;

    public Stats OwnerBaseStats => _baseStats;

    public Action OnHpRegenChange;

    public UnitStats(Stats baseStats)
    {
        _baseStats = baseStats;
    }

    public UnitStats()
    {

    }

    [SerializeField] private int maxHp;
    [SerializeField] private int baseDamage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float  baseRange;
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

    public int MaxHp => Mathf.RoundToInt(Mathf.Clamp((OwnerBaseStats.MaxHp.value + maxHp), 0, (OwnerBaseStats.MaxHp.value + maxHp)));
    public int BaseDamage => Mathf.RoundToInt(Mathf.Clamp((OwnerBaseStats.BaseDamage.value + baseDamage), 0, (OwnerBaseStats.BaseDamage.value + baseDamage)));
    public float AttackSpeed => Mathf.Clamp((OwnerBaseStats.AttackSpeed.value + attackSpeed), 0, 2f);
    public float BonusRange => Mathf.Clamp((OwnerBaseStats.BaseRange.value + baseRange), 0, (OwnerBaseStats.BaseRange.value + baseRange));
    public float MovementSpeed => Mathf.Clamp((OwnerBaseStats.MovementSpeed.value + movementSpeed), 0, (OwnerBaseStats.MovementSpeed.value + movementSpeed));
    public int CritDamage => Mathf.RoundToInt(Mathf.Clamp((OwnerBaseStats.CritDamage.value + critDamage), 0, (OwnerBaseStats.CritDamage.value + critDamage)));
    public int CritChance => Mathf.RoundToInt(Mathf.Clamp((OwnerBaseStats.CritChance.value + critChance), 0, (OwnerBaseStats.CritChance.value + critChance)));
    public int Armor => Mathf.RoundToInt(Mathf.Clamp((OwnerBaseStats.Armor.value + armor), 0, (OwnerBaseStats.Armor.value + armor)));

    public int HpRegen 
    { 
        get => Mathf.RoundToInt(Mathf.Clamp((OwnerBaseStats.HpRegen.value + hpRegen), 0, (OwnerBaseStats.HpRegen.value + hpRegen)));
        private set
        {
            hpRegen = value;
            OnHpRegenChange?.Invoke();
        }
    }
    public int AbilityProjectilePenetration => Mathf.RoundToInt(Mathf.Clamp((OwnerBaseStats.AbilityProjectilePenetration.value + abilityProjectilePenetration), 0, (OwnerBaseStats.AbilityProjectilePenetration.value + abilityProjectilePenetration)));
    public int Visibility => Mathf.Clamp((OwnerBaseStats.Visibility.value + visibility), 0, 1);
    public int ThreatLevel => Mathf.Clamp((OwnerBaseStats.ThreatLevel.value + threatLevel), 0, OwnerBaseStats.ThreatLevel.value + threatLevel);

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
                return OwnerBaseStats.MaxHp.value;
            case StatType.BaseDamage:
                return OwnerBaseStats.BaseDamage.value;
            case StatType.AttackSpeed:
                return OwnerBaseStats.AttackSpeed.value;
            case StatType.BaseRange:
                return OwnerBaseStats.BaseRange.value;
            case StatType.MovementSpeed:
                return OwnerBaseStats.MovementSpeed.value;
            case StatType.CritDamage:
                return OwnerBaseStats.CritDamage.value;
            case StatType.CritChance:
                return OwnerBaseStats.CritChance.value;
            case StatType.Armor:
                return OwnerBaseStats.Armor.value;
            case StatType.HpRegen:
                return OwnerBaseStats.HpRegen.value;
            case StatType.AbilityProjectilePenetration:
                return OwnerBaseStats.AbilityProjectilePenetration.value;
            case StatType.Visibility:
                return OwnerBaseStats.Visibility.value;
            case StatType.ThreatLevel:
                return OwnerBaseStats.ThreatLevel.value;
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
                OnStatChanged?.Invoke(statType, MaxHp);
                break;
            case StatType.BaseDamage:
                baseDamage += wholeValue;
                OnStatChanged?.Invoke(statType, BaseDamage);
                break;
            case StatType.AttackSpeed:
                attackSpeed += wholeValue;
                OnStatChanged?.Invoke(statType, AttackSpeed);
                break;
            case StatType.MovementSpeed:
                movementSpeed += wholeValue;
                OnStatChanged?.Invoke(statType, MovementSpeed);
                break;
            case StatType.CritDamage:
                critDamage += wholeValue;
                OnStatChanged?.Invoke(statType, CritDamage);
                break;
            case StatType.CritChance:
                critChance += wholeValue;
                OnStatChanged?.Invoke(statType, CritChance);
                break;
            case StatType.Armor:
                armor += wholeValue;
                OnStatChanged?.Invoke(statType, Armor);
                break;
            case StatType.HpRegen:
                HpRegen += wholeValue;
                OnStatChanged?.Invoke(statType, HpRegen);
                break;
            case StatType.AbilityProjectilePenetration:
                abilityProjectilePenetration += wholeValue;
                OnStatChanged?.Invoke(statType, AbilityProjectilePenetration);
                break;
            case StatType.Visibility:
                visibility += wholeValue;
                OnStatChanged?.Invoke(statType, Visibility);
                break;
            case StatType.ThreatLevel:
                threatLevel += wholeValue;
                OnStatChanged?.Invoke(statType, ThreatLevel);
                break;
        }

        
    }

    public void AddValueToStat(StatType statType, float decimalValue) //can be used to reduce or increase
    {
        switch (statType)
        {
            case StatType.AttackSpeed:
                attackSpeed += decimalValue;
                OnStatChanged?.Invoke(statType, AttackSpeed);
                break;
            case StatType.MovementSpeed:
                movementSpeed += decimalValue;
                OnStatChanged?.Invoke(statType, MovementSpeed);
                break;
            case StatType.BaseRange:
                baseRange += decimalValue;
                OnStatChanged?.Invoke(statType, BonusRange);
                break;
            default:
                AddValueToStat(statType, Mathf.RoundToInt(decimalValue));
                return;
        }
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
