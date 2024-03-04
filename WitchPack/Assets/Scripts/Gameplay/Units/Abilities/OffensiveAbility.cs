using Sirenix.OdinInspector;
using UnityEngine;

public abstract class OffensiveAbility : BaseAbility
{
    [BoxGroup("Offensive Ability")][SerializeField] private int baseDamage;
    [BoxGroup("Offensive Ability")][SerializeField] private float range;
    [BoxGroup("Offensive Ability")][SerializeField] private DamageBoostData[] damageBoosts;
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