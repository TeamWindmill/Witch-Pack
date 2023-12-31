using System.Collections.Generic;
using UnityEngine;

public class BaseAbility : ScriptableObject
{
    [SerializeField] private float cd;
    [SerializeField] private int baseDamage;
    [SerializeField] private List<StatusEffectConfig> statusEffects = new List<StatusEffectConfig>();

    public float Cd { get => cd; }
    public List<StatusEffectConfig> StatusEffects { get => statusEffects; }
    public int BaseDamage { get => baseDamage; }

    //cd
    //mana costs etc... 
    //every executable ability in the game inherits from this SO
}
