using TMPro;
using UnityEngine;

public class ShamanUpgradePanel : UIElement
{
    [SerializeField] private AbilityMetaUpgrade[] _abilityMetaUpgrades;
    [SerializeField] private StatMetaUpgrade _statMetaUpgrades;
    [SerializeField] private StatBarHandler _expStatBar;
    [SerializeField] private TextMeshProUGUI _skillPointsText;
    [SerializeField] private TextMeshProUGUI _levelText;

    private ShamanMetaUpgradeConfig _shamanMetaUpgradeConfig;
    public ShamanSaveData ShamanSaveData { get; private set; }

    public void Init(ShamanSaveData shamanSaveData)
    {
        ShamanSaveData = shamanSaveData;
        _expStatBar.Init(shamanSaveData);
        _skillPointsText.text = shamanSaveData.ShamanExperienceHandler.AvailableSkillPoints.ToString();
        _levelText.text = shamanSaveData.ShamanExperienceHandler.ShamanLevel.ToString();
        _shamanMetaUpgradeConfig = shamanSaveData.Config.ShamanMetaUpgradeConfig;
        for (int i = 0; i < _abilityMetaUpgrades.Length; i++)
        {
            _abilityMetaUpgrades[i].Init(this, _shamanMetaUpgradeConfig.AbilityPanelUpgrades[i], shamanSaveData.ShamanExperienceHandler.HasSkillPoints);
        }
        
        _statMetaUpgrades.Init(this, _shamanMetaUpgradeConfig.StatPanelUpgrades.StatUpgrades, shamanSaveData.ShamanExperienceHandler.HasSkillPoints);

        Show();
    }

    public void AddUpgradeToShaman(AbilityUpgradeConfig abilityUpgrade)
    {
        ShamanSaveData.AbilityUpgrades.Add(abilityUpgrade);
    }
    public void AddUpgradeToShaman(StatUpgradeConfig statUpgrade)
    {
        ShamanSaveData.StatUpgrades.Add(statUpgrade);
    }

    protected override void Update()
    {
        if (!isMouseOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Hide();
            }
        }
    }
}