using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShamanConfig", menuName = "ShamanConfig")]
public class ShamanConfig : BaseUnitConfig
{
    [SerializeField] private List<BaseAbility> rootAbilities = new List<BaseAbility>();
    [SerializeField] private List<BaseAbility> knownAbilities = new List<BaseAbility>();
    [SerializeField] private EnergyLevels energyLevels;
    [SerializeField] private bool isMale;


    public List<BaseAbility> KnownAbilities { get => knownAbilities; }
    public List<BaseAbility> RootAbilities { get => rootAbilities; }
    public EnergyLevels EnergyLevels => energyLevels;

    public bool IsMale => isMale;
}


