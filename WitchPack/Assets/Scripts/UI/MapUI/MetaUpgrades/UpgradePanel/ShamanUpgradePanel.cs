using Sirenix.Utilities;
using TMPro;
using UnityEngine;

public class ShamanUpgradePanel : UIElement
{
    [SerializeField] private AbilityMetaUpgrade[] _abilityMetaUpgrades;
    [SerializeField] private StatMetaUpgrade _statMetaUpgrades;

    private ShamanMetaUpgradeConfig _shamanMetaUpgradeConfig;
    public ShamanSaveData ShamanSaveData { get; private set; }

    public void Init(ShamanSaveData shamanSaveData)
    {
        Hide();
        ShamanSaveData = shamanSaveData;
        _shamanMetaUpgradeConfig = shamanSaveData.Config.ShamanMetaUpgradeConfig;
        for (int i = 0; i < _abilityMetaUpgrades.Length; i++)
        {
            _abilityMetaUpgrades[i].Init( _shamanMetaUpgradeConfig.AbilityPanelUpgrades[i]);
        }
        
        _statMetaUpgrades.Init(_shamanMetaUpgradeConfig.StatPanelUpgrades.StatUpgrades);

        Show();
    }

    public override void Refresh()
    {

        _abilityMetaUpgrades.ForEach(upgrade => upgrade.Refresh());
        _statMetaUpgrades.Refresh();
    }

    public override void Hide()
    {
        ShamanSaveData = null;
        base.Hide();
    }

    public void AddUpgradeToShaman(AbilityUpgradeConfig abilityUpgrade)
    {
        ShamanSaveData.AbilityUpgrades.Add(abilityUpgrade);
        UpgradeShaman(abilityUpgrade.SkillPointsCost);

    }
    public void AddUpgradeToShaman(StatMetaUpgradeConfig statMetaUpgrade)
    {
        ShamanSaveData.StatUpgrades.Add(statMetaUpgrade);
        UpgradeShaman(statMetaUpgrade.SkillPointsCost);
    }

    private void UpgradeShaman(int skillPointCost)
    {
        ShamanSaveData.ShamanExperienceHandler.UseSkillPoints(skillPointCost);
        Refresh();
    }
}