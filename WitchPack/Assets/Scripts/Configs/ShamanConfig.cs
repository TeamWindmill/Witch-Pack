using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShamanConfig", menuName = "ShamanConfig")]
public class ShamanConfig : ScriptableObject
{
    [SerializeField] private List<BaseAbility> knownAbilities = new List<BaseAbility>();
    [SerializeField] private StatSheet baseStats;

    public List<BaseAbility> KnownAbilities { get => knownAbilities; }
    public StatSheet BaseStats { get => baseStats; }
}
