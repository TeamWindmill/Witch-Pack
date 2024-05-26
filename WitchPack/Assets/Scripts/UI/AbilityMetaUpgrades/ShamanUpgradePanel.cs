using Sirenix.Utilities;
using UnityEngine;

public class ShamanUpgradePanel : UIElement
{
    [SerializeField] private AbilityMetaUpgrade[] _abilities;

    private ShamanMetaUpgradeConfig _shamanMetaUpgradeConfig;
    public void Init(ShamanSaveData shamanSaveData)
    {
        _shamanMetaUpgradeConfig = shamanSaveData.Config.ShamanMetaUpgradeConfig;
        for (int i = 0; i < _abilities.Length; i++)
        {
            _abilities[i].Init(_shamanMetaUpgradeConfig.AbilityPanelUpgrades[i],shamanSaveData.ExperienceHandler.HasExpPoints);
        }
        Show();
    }
}
