using Sirenix.OdinInspector;
using UnityEngine;


public abstract class OffensiveAbility : CastingAbility
{
    [BoxGroup("Offensive Ability")] [SerializeField]
    private AbilityStatInt baseDamage;

    [BoxGroup("Offensive Ability")] [SerializeField]
    private DamageBoostData[] damageBoosts;

    public DamageBoostData[] DamageBoosts => damageBoosts;

    public AbilityStatInt BaseDamage => baseDamage;
}