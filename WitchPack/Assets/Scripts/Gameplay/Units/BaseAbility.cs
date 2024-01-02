using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability")]
public class BaseAbility : ScriptableObject
{
    [SerializeField] private float cd;
    [SerializeField] private List<StatusEffectConfig> statusEffects = new List<StatusEffectConfig>();

    public float Cd { get => cd; }
    public List<StatusEffectConfig> StatusEffects { get => statusEffects; }

    public virtual void CastAbility(BaseUnit caster)
    {

    }

    //cd
    //mana costs etc... 
    //every executable ability in the game inherits from this SO
}
