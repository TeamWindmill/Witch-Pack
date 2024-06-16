public class StatMetaUpgradeIcon : MetaUpgradeIcon<StatUpgradeConfig>
{
    public override void Init(MetaUpgradeConfig upgradeConfig, bool hasSkillPoints)
    {
        _upgrade = upgradeConfig as StatUpgradeConfig;
        base.Init(upgradeConfig, hasSkillPoints);
    }
}