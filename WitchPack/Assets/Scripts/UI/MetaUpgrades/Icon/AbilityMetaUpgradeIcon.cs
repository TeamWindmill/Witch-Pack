public class AbilityMetaUpgradeIcon : MetaUpgradeIcon<AbilityUpgrade>
{
    public void Init(MetaUpgradeConfig upgradeConfig, AbilitySO[] abilitiesToUpgrade, bool hasSkillPoints)
    {
        _upgrade = new AbilityUpgrade(upgradeConfig as AbilityUpgradeConfig, abilitiesToUpgrade);

        base.Init(upgradeConfig, hasSkillPoints);
    }
}
