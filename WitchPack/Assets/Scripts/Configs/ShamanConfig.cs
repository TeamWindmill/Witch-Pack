using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShamanConfig", menuName = "ShamanConfig")]
public class ShamanConfig : BaseUnitConfig
{
    [SerializeField] private List<BaseAbility> rootAbilities = new List<BaseAbility>();
    [SerializeField] private List<BaseAbility> knownAbilities = new List<BaseAbility>();
    [SerializeField] private EnergyConfig _energyConfig;
    [SerializeField] private bool isMale;


    public List<BaseAbility> KnownAbilities { get => knownAbilities; }
    public List<BaseAbility> RootAbilities { get => rootAbilities; }
    public EnergyConfig EnergyConfig => _energyConfig;
    public bool IsMale => isMale;
}


