using Sirenix.Utilities;
using UnityEngine;

public class ShamanUpgradePanel : UIElement
{
    [SerializeField] private AbilityMetaUpgrade[] _abilities;

    private ShamanMetaUpgradeConfig _shamanMetaUpgradeConfig;
    private ShamanSaveData _shamanSaveData;

    public void Init(ShamanSaveData shamanSaveData)
    {
        _shamanSaveData = shamanSaveData;
        _shamanMetaUpgradeConfig = shamanSaveData.Config.ShamanMetaUpgradeConfig;
        for (int i = 0; i < _abilities.Length; i++)
        {
            _abilities[i].Init(this, _shamanMetaUpgradeConfig.AbilityPanelUpgrades[i], shamanSaveData.ExperienceHandler.HasSkillPoints);
        }

        Show();
    }

    public void AddUpgradeToShaman(AbilityUpgrade abilityUpgrade)
    {
        _shamanSaveData.AbilityUpgrades.Add(abilityUpgrade);
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