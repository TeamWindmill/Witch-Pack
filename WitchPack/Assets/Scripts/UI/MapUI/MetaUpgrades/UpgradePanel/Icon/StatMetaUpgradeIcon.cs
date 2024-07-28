using UnityEngine.EventSystems;

public class StatMetaUpgradeIcon : MetaUpgradeIcon<StatMetaUpgradeConfig>
{
    public override void Init(int index, MetaUpgradeConfig upgradeConfig, int availableSkillPoints)
    {
        Upgrade = upgradeConfig as StatMetaUpgradeConfig;
        base.Init(index,upgradeConfig, availableSkillPoints);
    }

    protected override void OnClick(PointerEventData eventData)
    {
        if (Upgrade.ShowAbility) OnSelect?.Invoke(_panelIndex,Upgrade.AbilitiesToUpgrade[0]);
        else
        {
            //highlight stat maybe?
        }
        
        base.OnClick(eventData);
    }
}