using UnityEngine;

public abstract class OffensiveAbility : BaseAbility
{
    [Header("Offensive Ability")]
    [SerializeField] private int baseDamage;
    [SerializeField] private float range;
    [SerializeField] private DamageBoostData[] damageBoosts;
    public int BaseDamage => baseDamage;
    public float Range => range;
    public DamageBoostData[] DamageBoosts => damageBoosts;
    public abstract bool CastAbility(BaseUnit caster);
    public abstract bool CheckCastAvailable(BaseUnit caster);
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