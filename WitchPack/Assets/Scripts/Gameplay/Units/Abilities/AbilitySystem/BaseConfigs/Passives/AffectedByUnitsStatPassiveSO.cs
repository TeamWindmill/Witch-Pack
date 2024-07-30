using Gameplay.Units.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Units.Abilities.AbilitySystem.BaseConfigs.Passives
{
    [CreateAssetMenu(fileName = "AffectedByUnitsStatPassive", menuName = "Ability/Passive/AffectedByUnitsStatPassive")]
    public class AffectedByUnitsStatPassiveSO : StatPassiveSO
    {
        [SerializeField] private bool _affectedByEnemies;

        [SerializeField] [ShowIf(nameof(_affectedByEnemies))]
        private bool _affectedByEnemiesWithStatusEffect;

        [SerializeField] [ShowIf(nameof(_affectedByEnemiesWithStatusEffect))]
        private StatusEffectVisual _enemyStatusEffect;

        [SerializeField] private bool _affectedByShamans;

        [SerializeField] [ShowIf(nameof(_affectedByShamans))]
        private bool _affectedByShamansWithStatusEffect;

        [SerializeField] [ShowIf(nameof(_affectedByShamansWithStatusEffect))]
        private StatusEffectVisual _shamanStatusEffect;
        public bool AffectedByEnemies => _affectedByEnemies;

        public bool AffectedByEnemiesWithStatusEffect => _affectedByEnemiesWithStatusEffect;

        public StatusEffectVisual EnemyStatusEffect => _enemyStatusEffect;
        public bool AffectedByShamans => _affectedByShamans;

        public bool AffectedByShamansWithStatusEffect => _affectedByShamansWithStatusEffect;

        public StatusEffectVisual ShamanStatusEffect => _shamanStatusEffect;
    }
}