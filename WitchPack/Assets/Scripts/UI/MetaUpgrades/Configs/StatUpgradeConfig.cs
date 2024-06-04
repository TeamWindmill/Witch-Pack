using UnityEngine;

[CreateAssetMenu(menuName = "MetaUpgrade/StatUpgrade",fileName = "StatUpgrade")]
public class StatUpgradeConfig : MetaUpgradeConfig
{
    [SerializeField] private StatType _statType;
    public StatType StatType => _statType;
}