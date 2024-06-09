public class AbilityMetaUpgradeIcon : MetaUpgradeIcon<AbilityUpgradeConfig>
{
    public void Init(MetaUpgradeConfig upgradeConfig, bool hasSkillPoints)
    {
        _upgrade = upgradeConfig as AbilityUpgradeConfig;

        base.Init(upgradeConfig, hasSkillPoints);
    }
}
