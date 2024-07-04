public class StatMetaUpgradeIcon : MetaUpgradeIcon<StatUpgradeConfig>
{
    public override void Init(MetaUpgradeConfig upgradeConfig, int availableSkillPoints)
    {
        Upgrade = upgradeConfig as StatUpgradeConfig;
        base.Init(upgradeConfig, availableSkillPoints);
    }
}