using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShamanConfig", menuName = "ShamanConfig")]
public class ShamanConfig : BaseUnitConfig
{
    [SerializeField] private List<BaseAbility> rootAbilities = new();
    [SerializeField] private List<BaseAbility> knownAbilities = new();
    [SerializeField] private EnergyConfig _energyConfig;
    [SerializeField] private Sex _sex;


    public List<BaseAbility> KnownAbilities { get => knownAbilities; }
    public List<BaseAbility> RootAbilities { get => rootAbilities; }
    public EnergyConfig EnergyConfig => _energyConfig;
    public Sex Sex => _sex;
}

public enum Sex
{
    Male,
    Female
}


