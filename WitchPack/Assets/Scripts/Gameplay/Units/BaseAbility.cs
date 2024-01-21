using System.Collections.Generic;
using UnityEngine;

public class BaseAbility : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Sprite icon;
    [SerializeField] private float cd;
    [SerializeField] private int penetration;
    [SerializeField, Tooltip("Interval before casting in real time")] private float castTime;
    [SerializeField] private List<StatusEffectConfig> statusEffects = new List<StatusEffectConfig>();

    public Sprite Icon => icon;
    public string Name => name;
    public float Cd => cd; 
    public List<StatusEffectConfig> StatusEffects { get => statusEffects; }
    public int Penetration { get => penetration; }

    public virtual bool CastAbility(BaseUnit caster)
    {
        return true;
    }

}
