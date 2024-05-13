using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShamanConfig", menuName = "ShamanConfig")]
public class ShamanConfig : BaseUnitConfig
{
    [SerializeField] private List<AbilitySO> rootAbilities = new();
    [SerializeField] private List<AbilitySO> knownAbilities = new();
    [SerializeField] private EnergyConfig _energyConfig;
    [SerializeField] private Sex _sex;


    public List<AbilitySO> KnownAbilities { get => knownAbilities; }
    public List<AbilitySO> RootAbilities { get => rootAbilities; }
    public EnergyConfig EnergyConfig => _energyConfig;
    public Sex Sex => _sex;
}

public enum Sex
{
    Male,
    Female
}


