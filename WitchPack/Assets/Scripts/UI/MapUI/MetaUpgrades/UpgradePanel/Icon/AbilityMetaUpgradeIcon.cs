public class AbilityMetaUpgradeIcon : MetaUpgradeIcon<AbilityUpgradeConfig>
{
    public override void Init(int index, MetaUpgradeConfig upgradeConfig, int availableSkillPoints)
    {
        Upgrade = upgradeConfig as AbilityUpgradeConfig;

        base.Init(index,upgradeConfig, availableSkillPoints);
    }
}
