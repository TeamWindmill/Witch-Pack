
public class StatMetaUpgradeIcon : MetaUpgradeIcon<StatUpgradeConfig>
{
        public void Init(MetaUpgradeConfig upgradeConfig, bool hasSkillPoints)
        {
                _upgrade = upgradeConfig as StatUpgradeConfig;
                base.Init(upgradeConfig, hasSkillPoints);
        }
}
