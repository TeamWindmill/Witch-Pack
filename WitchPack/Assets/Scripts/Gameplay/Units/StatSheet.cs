using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "StatSheet", menuName = "Stat Sheet")]
public class StatSheet : ScriptableObject
{
    //base stats here - affects every unit 
    public BaseStatWhole MaxHp = new BaseStatWhole(Stat.MaxHp);
    public BaseStatWhole BaseDamage = new BaseStatWhole(Stat.BaseDamage);
    public BaseStatDecimal AttackSpeed = new BaseStatDecimal(Stat.AttackSpeed);
    public BaseStatWhole BaseRange = new BaseStatWhole(Stat.BaseRange);
    public BaseStatWhole MovementSpeed = new BaseStatWhole(Stat.MovementSpeed);
    public BaseStatWhole CritDamage = new BaseStatWhole(Stat.CritDamage);
    public BaseStatWhole CritChance = new BaseStatWhole(Stat.CritChance);
    public BaseStatDecimal InvincibleTime = new BaseStatDecimal(Stat.InvincibleTime);
    public BaseStatWhole AbilityCooldownReduction = new BaseStatWhole(Stat.AbilityCooldownReduction);
    public BaseStatWhole Armor = new BaseStatWhole(Stat.Armor);
    public BaseStatWhole HpRegen = new BaseStatWhole(Stat.HpRegen);
    public BaseStatWhole BonusStatusEffectDuration = new BaseStatWhole(Stat.BonusStatusEffectDuration);
    public BaseStatWhole AbilityProjectileSpeed = new BaseStatWhole(Stat.AbilityProjectileSpeed);
    public BaseStatWhole AbilityProjectilePenetration = new BaseStatWhole(Stat.AbilityProjectilePenetration);
}

public enum Stat
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
public class BaseStatWhole
{
    [ReadOnly] public Stat stat;
    public int value;

    public BaseStatWhole(Stat givenStat)
    {
        stat = givenStat;
    }

}

[System.Serializable]
public class BaseStatDecimal
{
    [ReadOnly] public Stat stat;
    public float value;

    public BaseStatDecimal(Stat givenStat)
    {
        stat = givenStat;
    }

}
