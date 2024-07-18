public class StatMetaUpgradeIcon : MetaUpgradeIcon<StatMetaUpgradeConfig>
{
    public override void Init(int index, MetaUpgradeConfig upgradeConfig, int availableSkillPoints)
    {
        Upgrade = upgradeConfig as StatMetaUpgradeConfig;
        base.Init(index,upgradeConfig, availableSkillPoints);
    }
}