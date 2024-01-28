using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveAbility : BaseAbility
{
    [SerializeField] private int baseDamage;
    [SerializeField] private float range;
    public int BaseDamage { get => baseDamage; }
    public float Range { get => range;}
}
