using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbility : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Sprite icon;
    [SerializeField] private float cd;
    [SerializeField] private int penetration;

    [SerializeField, Tooltip("Interval before casting in real time")]
    private float castTime;

    [SerializeField] private List<StatusEffectConfig> statusEffects = new List<StatusEffectConfig>();
    [SerializeField] private BaseAbility[] _upgrades;
    [SerializeField] private TargetData targetData;
    private AbilityUpgradeState _abilityUpgradeState;


    public TargetData TargetData => targetData;
    public Sprite Icon => icon;
    public string Name => name;
    public float Cd => cd;
    public List<StatusEffectConfig> StatusEffects => statusEffects;
    public int Penetration => penetration;
    public BaseAbility[] Upgrades => _upgrades;
    public AbilityUpgradeState AbilityUpgradeState => _abilityUpgradeState;

    public virtual bool CastAbility(BaseUnit caster)
    {
        return true;
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