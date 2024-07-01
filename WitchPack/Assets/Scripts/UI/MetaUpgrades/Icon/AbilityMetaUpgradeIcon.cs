public class AbilityMetaUpgradeIcon : MetaUpgradeIcon<AbilityUpgradeConfig>
{
    public override void Init(MetaUpgradeConfig upgradeConfig, int availableSkillPoints)
    {
        Upgrade = upgradeConfig as AbilityUpgradeConfig;

        base.Init(upgradeConfig, availableSkillPoints);
    }
}
