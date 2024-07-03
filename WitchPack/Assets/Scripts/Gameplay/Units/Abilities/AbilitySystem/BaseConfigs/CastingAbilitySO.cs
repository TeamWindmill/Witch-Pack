using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class CastingAbilitySO : AbilitySO
{
    [BoxGroup("Casting")][SerializeField, Tooltip("Interval before casting in real time")] private float castTime;
    [BoxGroup("Casting")][SerializeField] private float cd;
    //[BoxGroup("Casting")][SerializeField] private float range; //currently not in use
    [BoxGroup("Casting")][SerializeField] private bool givesEnergyPoints;
    [BoxGroup("Casting")][SerializeField,ShowIf(nameof(givesEnergyPoints))] private int energyPoints;
    [BoxGroup("Casting")][SerializeField] private List<StatusEffectConfig> statusEffects = new();
    [BoxGroup("Casting")][SerializeField] private TargetData targetData;
    [BoxGroup("Casting")][SerializeField] private TargetData underAttackTargetData;

    [BoxGroup("Visual & Sound")][SerializeField] private bool _hasCastVisual;
    [BoxGroup("Visual & Sound")][SerializeField,ShowIf(nameof(_hasCastVisual))] private CastingHandsEffectType castVisualColor;
    [BoxGroup("Visual & Sound")][SerializeField] private bool _hasSFX = true;
    [BoxGroup("Visual & Sound")][SerializeField,ShowIf(nameof(_hasSFX))] private SoundEffectType soundEffectType;
    
    public float Cd => cd;
    //public float Range => range;
    public bool GivesEnergyPoints => givesEnergyPoints;
    public int EnergyPoints => energyPoints;
    public bool HasSfx => _hasSFX;
    public bool HasCastVisual => _hasCastVisual;
    public CastingHandsEffectType CastVisualColor => castVisualColor;
    public SoundEffectType SoundEffectType => soundEffectType;
    public float CastTime => castTime; 
    public TargetData TargetData => targetData;
    public TargetData UnderAttackTargetData => underAttackTargetData;
    public List<StatusEffectConfig> StatusEffects => statusEffects;
}

public enum DamageBonusType
{
    CurHp,
    MissingHp
}

[System.Serializable]
public struct DamageBoostData
{
    public DamageBonusType Type;
    public float damageBonusInPercent;
}