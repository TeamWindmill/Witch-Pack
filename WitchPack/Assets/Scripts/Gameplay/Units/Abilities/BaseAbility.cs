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
    
    
    
    [BoxGroup("Casting")][SerializeField, Tooltip("Interval before casting in real time")] private float castTime;
    [BoxGroup("Casting")][SerializeField] private float cd;
    [BoxGroup("Casting")][SerializeField] private bool givesEnergyPoints;
    [BoxGroup("Casting")][SerializeField,ShowIf(nameof(givesEnergyPoints))] private int energyPoints;
    
    [BoxGroup("Sound")][SerializeField] private bool _hasSFX = true;
    [BoxGroup("Sound")][SerializeField,ShowIf(nameof(_hasSFX))] private SoundEffectType soundEffectType;


    [BoxGroup("Casting")][SerializeField] private List<StatusEffectConfig> statusEffects = new List<StatusEffectConfig>();
    [BoxGroup("Casting")][SerializeField] private BaseAbility[] _upgrades;
    [BoxGroup("Casting")][SerializeField] private TargetData targetData;
    private AbilityUpgradeState _abilityUpgradeState;

    [BoxGroup("Popup")][SerializeField] private bool hasPopupColor;
    [BoxGroup("Popup")][SerializeField, ShowIf(nameof(hasPopupColor))] private Color popupColor;
    public bool HasPopupColor { get => hasPopupColor; }
    public Color PopupColor { get => popupColor; }


    public TargetData TargetData => targetData;
    public Sprite Icon => icon;
    public string Name => name;
    public string Discription => discription;
    public float Cd => cd;
    public bool GivesEnergyPoints => givesEnergyPoints;
    public int EnergyPoints => energyPoints;
    public List<StatusEffectConfig> StatusEffects => statusEffects;
    public BaseAbility[] Upgrades => _upgrades;
    public AbilityUpgradeState AbilityUpgradeState => _abilityUpgradeState;

    public bool HasSfx => _hasSFX;

    public SoundEffectType SoundEffectType => soundEffectType;


    public float CastTime { get => castTime; }



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