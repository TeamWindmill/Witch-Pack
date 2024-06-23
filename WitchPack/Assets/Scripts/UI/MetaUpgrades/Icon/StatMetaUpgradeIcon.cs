public class StatMetaUpgradeIcon : MetaUpgradeIcon<StatUpgradeConfig>
{
    public override void Init(MetaUpgradeConfig upgradeConfig, bool hasSkillPoints)
    {
        Upgrade = upgradeConfig as StatUpgradeConfig;
        base.Init(upgradeConfig, hasSkillPoints);
    }
}