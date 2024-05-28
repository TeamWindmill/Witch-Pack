using UnityEngine;

[CreateAssetMenu(menuName = "MetaUpgrade/AbilityUpgrade",fileName = "AbilityUpgrade")]
public class AbilityUpgradeConfig : MetaUpgradeConfig
{
    [SerializeField] private AbilityStatType _statType;
    public AbilityStatType StatType => _statType;
}


