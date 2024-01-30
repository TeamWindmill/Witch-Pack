using System.Collections.Generic;
using UnityEngine;

public class BaseAbility : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Sprite icon;
    [SerializeField] private float cd;
    [SerializeField] private int penetration;

    [SerializeField, Tooltip("Interval before casting in real time")]
    private float castTime;

    [SerializeField] private List<StatusEffectConfig> statusEffects = new List<StatusEffectConfig>();
    [SerializeField] private BaseAbility[] upgrades;
    [SerializeField] private TargetData targetData;

    public TargetData TargetData => targetData;
    public Sprite Icon => icon;
    public string Name => name;
    public float Cd => cd;
    public List<StatusEffectConfig> StatusEffects => statusEffects;
    public int Penetration => penetration;
    public BaseAbility[] Upgrades => upgrades;

    public virtual bool CastAbility(BaseUnit caster)
    {
        return true;
    }
}