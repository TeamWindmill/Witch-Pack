using UnityEngine;

[CreateAssetMenu(menuName = "MetaUpgrade/AbilityUpgrade",fileName = "AbilityUpgrade")]
public class AbilityUpgradeConfig : MetaUpgradeConfig
{
    [SerializeField] private AbilityStatType _statType;
    [SerializeField] private DamageBoostData[] _damageBoosts;
    [SerializeField] private AbilitySO[] _abilitiesToUpgrade;
    public AbilityStatType StatType => _statType;
    public AbilitySO[] AbilitiesToUpgrade => _abilitiesToUpgrade;
    public DamageBoostData[] DamageBoosts => _damageBoosts;
}


