using System.Collections.Generic;
using UnityEngine;

public class BaseAbility : ScriptableObject
{
    [SerializeField] private float cd;
    [SerializeField] private List<StatusEffectConfig> statusEffects = new List<StatusEffectConfig>();

    public float Cd { get => cd; }
    public List<StatusEffectConfig> StatusEffects { get => statusEffects; }

    public virtual bool CastAbility(BaseUnit caster)
    {
        return true;
    }

}
