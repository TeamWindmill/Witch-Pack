public class StatMetaUpgradeIcon : MetaUpgradeIcon<StatMetaUpgradeConfig>
{
    public override void Init(MetaUpgradeConfig upgradeConfig, int availableSkillPoints)
    {
        Upgrade = upgradeConfig as StatMetaUpgradeConfig;
        base.Init(upgradeConfig, availableSkillPoints);
    }
}