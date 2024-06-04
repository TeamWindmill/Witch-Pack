using UnityEngine;

public class ShamanUpgradePanel : UIElement
{
    [SerializeField] private AbilityMetaUpgrade[] _abilityMetaUpgrades;
    [SerializeField] private StatMetaUpgrade _statMetaUpgrades;

    private ShamanMetaUpgradeConfig _shamanMetaUpgradeConfig;
    private ShamanSaveData _shamanSaveData;

    public void Init(ShamanSaveData shamanSaveData)
    {
        _shamanSaveData = shamanSaveData;
        _shamanMetaUpgradeConfig = shamanSaveData.Config.ShamanMetaUpgradeConfig;
        for (int i = 0; i < _abilityMetaUpgrades.Length; i++)
        {
            _abilityMetaUpgrades[i].Init(this, _shamanMetaUpgradeConfig.AbilityPanelUpgrades[i], shamanSaveData.ExperienceHandler.HasSkillPoints);
        }
        
        _statMetaUpgrades.Init(this, _shamanMetaUpgradeConfig.StatPanelUpgrades.StatUpgrades, shamanSaveData.ExperienceHandler.HasSkillPoints);

        Show();
    }

    public void AddUpgradeToShaman(AbilityUpgradeConfig abilityUpgrade)
    {
        _shamanSaveData.AbilityUpgrades.Add(abilityUpgrade);
    }
    public void AddUpgradeToShaman(StatUpgradeConfig statUpgrade)
    {
        _shamanSaveData.StatUpgrades.Add(statUpgrade);
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