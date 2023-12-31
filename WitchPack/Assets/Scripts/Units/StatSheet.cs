using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "StatSheet", menuName = "Stat Sheet")]
public class StatSheet : ScriptableObject
{
    //base stats here - affects every unit 
    public BaseStat MaxHp = new BaseStat(Stat.MaxHp);
    public BaseStat BaseDamage = new BaseStat(Stat.BaseDamage);
    public BaseStat AttackSpeed = new BaseStat(Stat.AttackSpeed);
    public BaseStat BonusRange = new BaseStat(Stat.BonusRange);
    public BaseStat MovementSpeed = new BaseStat(Stat.MovementSpeed);
    public BaseStat CritDamage = new BaseStat(Stat.CritDamage);
    public BaseStat CritChance = new BaseStat(Stat.CritChance);
    public BaseStat InvincibleTime = new BaseStat(Stat.InvincibleTime);
    public BaseStat AbilityCooldownReduction = new BaseStat(Stat.AbilityCooldownReduction);
    public BaseStat Armor = new BaseStat(Stat.Armor);
    public BaseStat HpRegen = new BaseStat(Stat.HpRegen);
    public BaseStat BonusStatusEffectDuration = new BaseStat(Stat.BonusStatusEffectDuration);
}

public enum Stat
{
    MaxHp,
    BaseDamage,//affects every damage instance a unit deals in the game
    AttackSpeed,//attack speed for auto attacks only
    BonusRange,//range increase for every ranged attack 
    MovementSpeed,
    CritDamage,
    CritChance,
    InvincibleTime,//flat duration of invincibility after recieving damage
    AbilityCooldownReduction,//cdr for abilities only (anything that isnt an auto attack)
    Armor,// damage redcutcion from all sources
    HpRegen,//amount of health resotred every second while being out of combat
    BonusStatusEffectDuration//fixed duration added for every effect applied by unit
}

[System.Serializable]
public class BaseStat
{
    [ReadOnly] public Stat stat;
    public int value;

    public BaseStat(Stat givenStat)
    {
        stat = givenStat;
    }

}