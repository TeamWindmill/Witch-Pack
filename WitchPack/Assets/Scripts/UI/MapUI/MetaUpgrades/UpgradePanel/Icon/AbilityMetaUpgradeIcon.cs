using UI.MapUI.MetaUpgrades.UpgradePanel.Configs;
using UnityEngine.EventSystems;

namespace UI.MapUI.MetaUpgrades.UpgradePanel.Icon
{
    public class AbilityMetaUpgradeIcon : MetaUpgradeIcon<AbilityUpgradeConfig>
    {
        public override void Init(int index, MetaUpgradeConfig upgradeConfig, int availableSkillPoints)
        {
            Upgrade = upgradeConfig as AbilityUpgradeConfig;

            base.Init(index,upgradeConfig, availableSkillPoints);
        }

        protected override void OnClick(PointerEventData eventData)
        {
            OnSelect?.Invoke(_panelIndex,Upgrade.AbilitiesToUpgrade[0],Upgrade);
            SelectIcon(true);
            base.OnClick(eventData);
        }
    }
}
