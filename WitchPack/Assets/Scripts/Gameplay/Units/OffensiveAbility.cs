using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveAbility : BaseAbility
{
    [SerializeField] private int baseDamage;
    [SerializeField] private float range;
    [SerializeField] private TargetData targetData;
    public int BaseDamage { get => baseDamage; }
    public TargetData TargetData { get => targetData; }
    public float Range { get => range;}
}
