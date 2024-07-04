using Sirenix.Utilities;
using TMPro;
using UnityEngine;

public class ShamanUpgradePanel : UIElement
{
    [SerializeField] private AbilityMetaUpgrade[] _abilityMetaUpgrades;
    [SerializeField] private StatMetaUpgrade _statMetaUpgrades;
    [SerializeField] private StatBar _expStatBar;
    [SerializeField] private TextMeshProUGUI _skillPointsText;
    [SerializeField] private TextMeshProUGUI _levelText;

    private ShamanMetaUpgradeConfig _shamanMetaUpgradeConfig;
    public ShamanSaveData ShamanSaveData { get; private set; }

    public void Init(ShamanSaveData shamanSaveData)
    {
        Hide();
        ShamanSaveData = shamanSaveData;
        _shamanMetaUpgradeConfig = shamanSaveData.Config.ShamanMetaUpgradeConfig;
        var statbarData = new StatBarData("Exp", shamanSaveData.ShamanExperienceHandler.CurrentExp, shamanSaveData.ShamanExperienceHandler.MaxExpToNextLevel);
        shamanSaveData.ShamanExperienceHandler.OnShamanGainExp += _expStatBar.UpdateStatbar;
        _expStatBar.Init(statbarData);
        _skillPointsText.text = shamanSaveData.ShamanExperienceHandler.AvailableSkillPoints.ToString();
        _levelText.text = shamanSaveData.ShamanExperienceHandler.ShamanLevel.ToString();
        for (int i = 0; i < _abilityMetaUpgrades.Length; i++)
        {
            _abilityMetaUpgrades[i].Init( _shamanMetaUpgradeConfig.AbilityPanelUpgrades[i]);
        }
        
        _statMetaUpgrades.Init(_shamanMetaUpgradeConfig.StatPanelUpgrades.StatUpgrades);

        Show();
    }

    public override void Refresh()
    {
        _skillPointsText.text = ShamanSaveData.ShamanExperienceHandler.AvailableSkillPoints.ToString();
        _levelText.text = ShamanSaveData.ShamanExperienceHandler.ShamanLevel.ToString();

        _abilityMetaUpgrades.ForEach(upgrade => upgrade.Refresh());
        _statMetaUpgrades.Refresh();
    }

    public override void Hide()
    {
        if(ShamanSaveData != null) ShamanSaveData.ShamanExperienceHandler.OnShamanGainExp -= _expStatBar.UpdateStatbar;
        _expStatBar.Hide();
        ShamanSaveData = null;
        base.Hide();
    }

    public void AddUpgradeToShaman(AbilityUpgradeConfig abilityUpgrade)
    {
        ShamanSaveData.AbilityUpgrades.Add(abilityUpgrade);
        UpgradeShaman(abilityUpgrade.SkillPointsCost);

    }
    public void AddUpgradeToShaman(StatUpgradeConfig statUpgrade)
    {
        ShamanSaveData.StatUpgrades.Add(statUpgrade);
        UpgradeShaman(statUpgrade.SkillPointsCost);
    }

    private void UpgradeShaman(int skillPointCost)
    {
        ShamanSaveData.ShamanExperienceHandler.UseSkillPoints(skillPointCost);
        Refresh();
    }
}