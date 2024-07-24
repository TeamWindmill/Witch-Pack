using UnityEngine.EventSystems;

public class AbilityMetaUpgradeIcon : MetaUpgradeIcon<AbilityUpgradeConfig>
{
    public override void Init(int index, MetaUpgradeConfig upgradeConfig, int availableSkillPoints)
    {
        Upgrade = upgradeConfig as AbilityUpgradeConfig;

        base.Init(index,upgradeConfig, availableSkillPoints);
    }

    protected override void OnClick(PointerEventData eventData)
    {
        OnSelect?.Invoke(_panelIndex,Upgrade.AbilitiesToUpgrade[0]);
        base.OnClick(eventData);
    }
}
