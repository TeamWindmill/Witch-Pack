using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Level/Challenge",fileName = "LevelChallenge")]
public class LevelChallenge : ScriptableObject
{
    [SerializeField] private LevelChallengeType _challengeType;
    [SerializeField] private string _displayName;
    [SerializeField] private string[] _bonusesDescription;
    [SerializeField,HideIf(nameof(_challengeType),LevelChallengeType.None)] private StatValueUpgradeConfig[] _statUpgrades;
    [SerializeField] private int _reduceShamanSlots;
    [SerializeField] private float _expMultiplier;
    [SerializeField] private float _firstTimeExpMultiplier;

    public LevelChallengeType ChallengeType => _challengeType;
    public string DisplayName => _displayName;
    public string[] BonusesDescription => _bonusesDescription;
    public StatValueUpgradeConfig[] StatUpgrades => _statUpgrades;
    public int ReduceShamanSlots => _reduceShamanSlots;
    public float ExpMultiplier => 1 + _expMultiplier;
    public float FirstTimeExpMultiplier => 1 + _firstTimeExpMultiplier;
}

public enum LevelChallengeType
{
    None,
    AffectEnemies,
    AffectShamans,
    AffectBoth
}