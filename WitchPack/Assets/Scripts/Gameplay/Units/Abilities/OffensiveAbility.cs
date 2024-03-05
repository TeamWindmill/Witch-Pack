using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Units.Abilities
{
    public abstract class OffensiveAbility : CastingAbility
    {
        [BoxGroup("Offensive Ability")][SerializeField] private int baseDamage;
        [BoxGroup("Offensive Ability")][SerializeField] private DamageBoostData[] damageBoosts;
        public int BaseDamage => baseDamage;
        public DamageBoostData[] DamageBoosts => damageBoosts;
    }
}