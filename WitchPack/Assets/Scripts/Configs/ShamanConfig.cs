using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShamanConfig", menuName = "ShamanConfig")]
public class ShamanConfig : BaseUnitConfig
{
    [SerializeField] private List<BaseAbility> rootAbilities = new List<BaseAbility>();
    [SerializeField] private List<BaseAbility> knownAbilities = new List<BaseAbility>();
    

    public List<BaseAbility> KnownAbilities { get => knownAbilities; }
    public List<BaseAbility> RootAbilities { get => rootAbilities; }
}
