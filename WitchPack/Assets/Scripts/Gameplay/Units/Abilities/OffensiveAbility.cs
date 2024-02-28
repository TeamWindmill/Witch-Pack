using UnityEngine;

public class OffensiveAbility : BaseAbility
{
    [Header("Offensive Ability")]
    [SerializeField] private int baseDamage;
    [SerializeField] private float range;
    [SerializeField] private DamageBoostData[] damageBoosts;
    public int BaseDamage => baseDamage;
    public float Range => range;
    public DamageBoostData[] DamageBoosts => damageBoosts;
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