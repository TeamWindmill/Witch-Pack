using UnityEngine;

public class OffensiveAbility : BaseAbility
{
    [SerializeField] private int baseDamage;
    [SerializeField] private float range;
    [SerializeField] private DamageBoostData[] damageBoosts;
    public int BaseDamage { get => baseDamage; }
    public float Range { get => range; }
    public DamageBoostData[] DamageBoosts { get => damageBoosts; }
}

public enum DamageBonusType
{
    CurHp,
    MissingHp
}

[System.Serializable]
public struct DamageBoostData
{
    public DamageBonusType Type;
    [Tooltip("%hp threshold to reach (according to type)")] public float Threshold;
    public float damageBonus;
}