using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class BaseAbilitySO : ScriptableObject
{
    [BoxGroup("General Settings/left/Details",centerLabel: true)]
    [LabelWidth(50)][VerticalGroup("General Settings/left")]
    [HorizontalGroup("General Settings")]
    [SerializeField] private string name;
    
    [BoxGroup("General Settings/left/Details",centerLabel: true)]
    [TextArea(4, 14)][VerticalGroup("General Settings/left")]
    [HorizontalGroup("General Settings")]
    [SerializeField] private string discription;
    
    [PreviewField(63)]
    [BoxGroup("General Settings/Icons",centerLabel: true)]
    [HorizontalGroup("General Settings")]
    [SerializeField] private Sprite defaultIcon;
    
    [PreviewField(63)]
    [BoxGroup("General Settings/Icons",centerLabel: true)]
    [HorizontalGroup("General Settings")]
    [SerializeField] private Sprite disabledIcon;
    
    [PreviewField(63)]
    [BoxGroup("General Settings/Icons",centerLabel: true)]
    [HorizontalGroup("General Settings")]
    [SerializeField] private Sprite upgradeIcon;
    
    [HorizontalGroup("General Settings")][VerticalGroup("General Settings/left")]
    [BoxGroup("General Settings/left/Skill Tree")][SerializeField] private BaseAbilitySO[] _upgrades;
    
    [BoxGroup("Popup Numbers")][SerializeField] private bool hasPopupColor;
    [BoxGroup("Popup Numbers")][SerializeField, ShowIf(nameof(hasPopupColor))] private Color popupColor;

    private AbilityUpgradeState _abilityUpgradeState;
    public bool HasPopupColor { get => hasPopupColor; }
    public Color PopupColor { get => popupColor; }
    public Sprite DefaultIcon => defaultIcon;
    public Sprite DisabledIcon => disabledIcon;
    public Sprite UpgradeIcon => upgradeIcon;
    public string Name => name;
    public string Discription => discription;
    public BaseAbilitySO[] Upgrades => _upgrades;
    public AbilityUpgradeState AbilityUpgradeState => _abilityUpgradeState;

    public virtual void OnSetCaster(BaseUnit caster)
    {

    }

    public void ChangeUpgradeState(AbilityUpgradeState state)
    {
        _abilityUpgradeState = state;
    }

    public void UpgradeAbility()
    {
        if (_abilityUpgradeState != AbilityUpgradeState.Open) return;
        ChangeUpgradeState(AbilityUpgradeState.Upgraded);
        foreach (var abilityUpgrade in _upgrades)
        {
            abilityUpgrade.ChangeUpgradeState(AbilityUpgradeState.Open);
        }
    }

    public List<BaseAbilitySO> GetUpgrades()
    {
        var upgrades = new List<BaseAbilitySO>();
        foreach (var upgrade in _upgrades)
        {
            upgrades.Add(upgrade);
            foreach (var secondUpgrade in upgrade.Upgrades)
            {
                upgrades.Add(secondUpgrade);
            }
        }

        return upgrades;
    }


}