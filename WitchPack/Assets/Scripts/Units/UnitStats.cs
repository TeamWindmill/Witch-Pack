using UnityEngine;

[System.Serializable]
public class UnitStats
{

    private BaseUnit owner;

    private StatSheet ownerBaseStats => owner.BaseStats;

    private int maxHp;
    private int baseDamage;
    private int attackSpeed;
    private int bonusRange;
    private int movementSpeed;
    private int critDamage;
    private int critChance;
    private int invincibleTime;
    private int abilityCooldownReduction;
    private int armor;
    private int hpRegen;
    private int bonusStatusEffectDuration;

    public int MaxHp { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.MaxHp.value + maxHp), 0, (ownerBaseStats.MaxHp.value + maxHp))); } }
    public int BaseDamage { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.BaseDamage.value + baseDamage), 0, (ownerBaseStats.BaseDamage.value + baseDamage))); } }
    public int AttackSpeed { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.AttackSpeed.value + attackSpeed), 0, (ownerBaseStats.AttackSpeed.value + attackSpeed))); } }
    public int BonusRange { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.BonusRange.value + bonusRange), 0, (ownerBaseStats.BonusRange.value + bonusRange))); } }
    public int MovementSpeed { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.MovementSpeed.value + movementSpeed), 0, (ownerBaseStats.MovementSpeed.value + movementSpeed))); } }
    public int CritDamage { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.CritDamage.value + critDamage), 0, (ownerBaseStats.CritDamage.value + critDamage))); } }
    public int CritChance { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.CritChance.value + critChance), 0, (ownerBaseStats.CritChance.value + critChance))); } }
    public int InvincibleTime { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.InvincibleTime.value + invincibleTime), 0, (ownerBaseStats.InvincibleTime.value + invincibleTime))); } }
    public int AbilityCooldownReduction { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.AbilityCooldownReduction.value + abilityCooldownReduction), 0, (ownerBaseStats.AbilityCooldownReduction.value + abilityCooldownReduction))); } }
    public int Armor { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.Armor.value + armor), 0, (ownerBaseStats.Armor.value + armor))); } }
    public int HpRegen { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.HpRegen.value + hpRegen), 0, (ownerBaseStats.HpRegen.value + hpRegen))); } }
    public int BonusStatusEffectDuration { get { return Mathf.RoundToInt(Mathf.Clamp((ownerBaseStats.BonusStatusEffectDuration.value + bonusStatusEffectDuration), 0, (ownerBaseStats.BonusStatusEffectDuration.value + bonusStatusEffectDuration))); } }

    public void CacheOwner(BaseUnit owner)
    {
        this.owner = owner;
    }

    public void AddValueToStat(Stat stat, int value) //can be used to reduce or increase
    {
        switch (stat)
        {
            case Stat.MaxHp:
                maxHp += value;
                break;
            case Stat.BaseDamage:
                baseDamage += value;
                break;
            case Stat.AttackSpeed:
                attackSpeed += value;
                break;
            case Stat.BonusRange:
                bonusRange += value;
                break;
            case Stat.MovementSpeed:
                movementSpeed += value;
                break;
            case Stat.CritDamage:
                critDamage += value;
                break;
            case Stat.CritChance:
                critChance += value;
                break;
            case Stat.InvincibleTime:
                invincibleTime += value;
                break;
            case Stat.AbilityCooldownReduction:
                abilityCooldownReduction += value;
                break;
            case Stat.Armor:
                armor += value;
                break;
            case Stat.HpRegen:
                hpRegen += value;
                break;
            case Stat.BonusStatusEffectDuration:
                bonusStatusEffectDuration += value;
                break;
            default:
                break;
        }
    }






}
