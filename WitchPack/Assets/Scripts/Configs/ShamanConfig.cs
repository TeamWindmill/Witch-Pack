using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShamanConfig", menuName = "ShamanConfig")]
public class ShamanConfig : BaseUnitConfig
{
    [SerializeField] private Sex _sex;
    [SerializeField] private List<AbilitySO> rootAbilities = new();
    [SerializeField] private List<AbilitySO> knownAbilities = new();
    [SerializeField] private EnergyConfig _energyConfig;
    [SerializeField] private ShamanExperienceConfig _shamanExperienceConfig;
    [SerializeField] private ShamanMetaUpgradeConfig _shamanMetaUpgradeConfig;


    public ShamanMetaUpgradeConfig ShamanMetaUpgradeConfig => _shamanMetaUpgradeConfig;
    public List<AbilitySO> KnownAbilities { get => knownAbilities; }
    public List<AbilitySO> RootAbilities { get => rootAbilities; }
    public EnergyConfig EnergyConfig => _energyConfig;
    public ShamanExperienceConfig ShamanExperienceConfig => _shamanExperienceConfig;
    public Sex Sex => _sex;
}

public enum Sex
{
    Male,
    Female
}


