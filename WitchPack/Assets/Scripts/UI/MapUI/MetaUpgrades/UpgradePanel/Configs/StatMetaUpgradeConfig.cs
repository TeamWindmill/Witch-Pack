using System;
using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using Gameplay.Units.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI.MapUI.MetaUpgrades.UpgradePanel.Configs
{
    [Serializable]
    public class StatMetaUpgradeConfig : MetaUpgradeConfig
    {
        [SerializeField] private bool _upgradeAbility;
        [SerializeField] private bool _upgradePassiveAbility;
        [SerializeField,HideIf(nameof(_upgradeAbility))] private StatUpgrade[] _stats;
    
        [SerializeField,ShowIf(nameof(ShowAbility))] private AbilityStatUpgradeConfig[] _abilityStats;
        [SerializeField,ShowIf(nameof(ShowAbility))] private AbilityBehavior[] _abilitiesBehaviors;
        [SerializeField,ShowIf(nameof(ShowAbility))] private DamageBoostData[] _damageBoosts;
        [SerializeField,ShowIf(nameof(ShowAbility))] private StatusEffectUpgradeConfig[] _statusEffectUpgrades;
        [SerializeField,ShowIf(nameof(ShowAbility))] private AbilitySO[] _abilitiesToUpgrade;
        public bool ShowAbility =>  _upgradePassiveAbility || _upgradeAbility;
        public StatUpgrade[] Stats => _stats;
        public AbilityStatUpgradeConfig[] AbilityStats => _abilityStats;
        public bool UpgradeAbility => _upgradeAbility;
        public bool UpgradePassiveAbility => _upgradePassiveAbility;
        public AbilitySO[] AbilitiesToUpgrade => _abilitiesToUpgrade;
        public DamageBoostData[] DamageBoosts => _damageBoosts;
        public AbilityBehavior[] AbilitiesBehaviors => _abilitiesBehaviors;
        public StatusEffectUpgradeConfig[] StatusEffectUpgrades => _statusEffectUpgrades;
    }

    [Serializable]
    public struct StatUpgrade
    {
        [SerializeField] private StatType _statType;
        [SerializeField] private float _statValue;
        [SerializeField] private Factor _factor;

        public StatType StatType => _statType;
        public float StatValue => _statValue;
        public Factor Factor => _factor;
    }
}