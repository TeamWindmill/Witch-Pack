using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class BaseAbility : ScriptableObject
{
    [HideLabel, PreviewField(55,ObjectFieldAlignment.Left)]
    [BoxGroup("General Settings")]
    [HorizontalGroup("General Settings/Split",width:70)]
    [SerializeField] private Sprite icon;
    
    [BoxGroup("General Settings")]
    [LabelWidth(50)]
    [VerticalGroup("General Settings/Split/Right")]
    [HorizontalGroup("General Settings/Split")]
    [SerializeField] private string name;
    
    [BoxGroup("General Settings")]
    [TextArea(4, 14)]
    [HorizontalGroup("General Settings/Split")]
    [VerticalGroup("General Settings/Split/Right")]
    [SerializeField] private string discription;

    [BoxGroup("Skill Tree")][SerializeField] private BaseAbility[] _upgrades;
    [BoxGroup("Damage Popup Numbers")][SerializeField] private bool hasPopupColor;
    [BoxGroup("Damage Popup Numbers")][SerializeField, ShowIf(nameof(hasPopupColor))] private Color popupColor;

    private AbilityUpgradeState _abilityUpgradeState;
    public bool HasPopupColor { get => hasPopupColor; }
    public Color PopupColor { get => popupColor; }
    public Sprite Icon => icon;
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