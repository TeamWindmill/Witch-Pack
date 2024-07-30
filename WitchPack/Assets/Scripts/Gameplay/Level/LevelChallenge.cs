using System;
using Sirenix.OdinInspector;
using UI.MapUI.MetaUpgrades.UpgradePanel.Configs;
using UnityEngine;

namespace Gameplay.Level
{
    [Serializable]
    public struct LevelChallenge
    {
        public int Index { get; set; }
        [SerializeField] private LevelChallengeType _challengeType;
        [SerializeField] private string _displayName;
        [SerializeField] private string[] _bonusesDescription;
        [SerializeField,HideIf(nameof(_challengeType),LevelChallengeType.None)] private StatUpgrade[] _statUpgrades;
        [SerializeField] private int _reduceShamanSlots;
        [SerializeField] private float _expMultiplier;

        public LevelChallengeType ChallengeType => _challengeType;
        public string DisplayName => _displayName;
        public string[] BonusesDescription => _bonusesDescription;
        public StatUpgrade[] StatUpgrades => _statUpgrades;
        public int ReduceShamanSlots => _reduceShamanSlots;
        public float ExpMultiplier => 1 + _expMultiplier;
    }

    public enum LevelChallengeType
    {
        None,
        AffectEnemies,
        AffectShamans,
        AffectBoth
    }
}