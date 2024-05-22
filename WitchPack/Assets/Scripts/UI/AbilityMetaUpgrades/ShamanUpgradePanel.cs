using Sirenix.Utilities;
using UnityEngine;

public class ShamanUpgradePanel : UIElement
{
    [SerializeField] private AbilityMetaUpgrade[] _abilities;

    private ShamanMetaUpgradeConfig _shamanMetaUpgradeConfig;
    public void Init(ShamanMetaUpgradeConfig shamanMetaUpgradeConfig)
    {
        _shamanMetaUpgradeConfig = shamanMetaUpgradeConfig;
        for (int i = 0; i < _abilities.Length; i++)
        {
            _abilities[i].Init(shamanMetaUpgradeConfig.AbilityPanelUpgrades[i]);
        }
        Show();
    }
}
