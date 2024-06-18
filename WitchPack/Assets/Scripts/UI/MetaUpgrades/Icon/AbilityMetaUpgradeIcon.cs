public class AbilityMetaUpgradeIcon : MetaUpgradeIcon<AbilityUpgradeConfig>
{
    public override void Init(MetaUpgradeConfig upgradeConfig, bool hasSkillPoints)
    {
        Upgrade = upgradeConfig as AbilityUpgradeConfig;

        base.Init(upgradeConfig, hasSkillPoints);
    }
}
