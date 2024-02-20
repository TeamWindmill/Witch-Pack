using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BaseAbility : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private string discription;
    [SerializeField] private Sprite icon;
    [SerializeField] private float cd;
    [SerializeField] private bool isPassive;
    [SerializeField] private bool givesEnergyPoints;
    [SerializeField,ShowIf(nameof(givesEnergyPoints))] private int energyPoints;
    [SerializeField] private bool _hasSFX = true;
    [SerializeField,ShowIf(nameof(_hasSFX))] private SoundEffectType soundEffectType;

    [SerializeField, Tooltip("Interval before casting in real time")]
    private float castTime;

    [SerializeField] private List<StatusEffectConfig> statusEffects = new List<StatusEffectConfig>();
    [SerializeField] private BaseAbility[] _upgrades;
    [SerializeField] private TargetData targetData;
    private AbilityUpgradeState _abilityUpgradeState;


    public TargetData TargetData => targetData;
    public Sprite Icon => icon;
    public string Name => name;
    public string Discription => discription;
    public float Cd => cd;
    public bool IsPassive => isPassive;
    public bool GivesEnergyPoints => givesEnergyPoints;
    public int EnergyPoints => energyPoints;
    public List<StatusEffectConfig> StatusEffects => statusEffects;
    public BaseAbility[] Upgrades => _upgrades;
    public AbilityUpgradeState AbilityUpgradeState => _abilityUpgradeState;

    public bool HasSfx => _hasSFX;

    public SoundEffectType SoundEffectType => soundEffectType;


    public float CastTime { get => castTime; }

    public virtual bool CastAbility(BaseUnit caster)
    {
        return true;
    }

    public virtual bool CheckCastAvailable(BaseUnit caster)
    {
        return true;
    }

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