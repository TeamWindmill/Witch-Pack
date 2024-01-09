using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "StatSheet", menuName = "Stat Sheet")]
public class StatSheet : ScriptableObject
{
    [SerializeField, ReadOnly] private float damageMitigation;

    [Button]
    public void CalcDamageMitigation()
    {
        float damageTaken = 100 * 100 / (100 + Armor.value);
        damageMitigation = (1 - (damageTaken / 100)) * 100;
    }

    //base stats here - affects every unit 
    public BaseStat MaxHp = new BaseStat(StatType.MaxHp);
    public BaseStat BaseDamage = new BaseStat(StatType.BaseDamage);
    public BaseStatDecimal AttackSpeed = new BaseStatDecimal(StatType.AttackSpeed);//capped at unit stats 
    public BaseStat BaseRange = new BaseStat(StatType.BaseRange);
    public BaseStat MovementSpeed = new BaseStat(StatType.MovementSpeed);
    public BaseStat CritDamage = new BaseStat(StatType.CritDamage);
    public BaseStat CritChance = new BaseStat(StatType.CritChance);
    public BaseStatDecimal InvincibleTime = new BaseStatDecimal(StatType.InvincibleTime);
    public BaseStat AbilityCooldownReduction = new BaseStat(StatType.AbilityCooldownReduction);
    public BaseStat Armor = new BaseStat(StatType.Armor);
    public BaseStat HpRegen = new BaseStat(StatType.HpRegen);
    public BaseStat BonusStatusEffectDuration = new BaseStat(StatType.BonusStatusEffectDuration);
    public BaseStat AbilityProjectileSpeed = new BaseStat(StatType.AbilityProjectileSpeed);
    public BaseStat AbilityProjectilePenetration = new BaseStat(StatType.AbilityProjectilePenetration);

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
    AbilityProjectilePenetration//the amount of times a projectile ability can hit targets before disabling

}

[System.Serializable]
public class BaseStat
{
    [ReadOnly] public StatType statType;
    public int value;

    public BaseStat(StatType givenStatType)
    {
        statType = givenStatType;
    }

}

[System.Serializable]
public class BaseStatDecimal
{
    [ReadOnly] public StatType statType;
    public float value;

    public BaseStatDecimal(StatType givenStatType)
    {
        statType = givenStatType;
    }

}
