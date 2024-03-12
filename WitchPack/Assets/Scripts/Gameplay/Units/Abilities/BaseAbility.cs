using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class BaseAbility : ScriptableObject
{
    [BoxGroup("General Settings/Details",centerLabel: true)]
    [LabelWidth(50)]
    //[VerticalGroup("General Settings/Split/Right")]
    [HorizontalGroup("General Settings")]
    [SerializeField] private string name;
    
    [BoxGroup("General Settings/Details",centerLabel: true)]
    [TextArea(4, 14)]
    [HorizontalGroup("General Settings")]
    //[VerticalGroup("General Settings/Split/Right")]
    [SerializeField] private string discription;
    
    [PreviewField(55)]
    [BoxGroup("General Settings/Icons",centerLabel: true)]
    [HorizontalGroup("General Settings")]
    [SerializeField] private Sprite defaultIcon;
    
    [PreviewField(55)]
    [BoxGroup("General Settings/Icons",centerLabel: true)]
    [HorizontalGroup("General Settings")]
    [SerializeField] private Sprite disabledIcon;
    
    [PreviewField(55)]
    [BoxGroup("General Settings/Icons",centerLabel: true)]
    [HorizontalGroup("General Settings")]
    [SerializeField] private Sprite upgradeIcon;

    [BoxGroup("Skill Tree")][SerializeField] private BaseAbility[] _upgrades;
    [BoxGroup("Damage Popup Numbers")][SerializeField] private bool hasPopupColor;
    [BoxGroup("Damage Popup Numbers")][SerializeField, ShowIf(nameof(hasPopupColor))] private Color popupColor;

    private AbilityUpgradeState _abilityUpgradeState;
    public bool HasPopupColor { get => hasPopupColor; }
    public Color PopupColor { get => popupColor; }
    public Sprite DefaultIcon => defaultIcon;
    public string Name => name;
    public string Discription => discription;
    public BaseAbility[] Upgrades => _upgrades;
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

    public List<BaseAbility> GetUpgrades()
    {
        var upgrades = new List<BaseAbility>();
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