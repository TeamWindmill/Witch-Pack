using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnitConfig : ScriptableObject
{
    [SerializeField] private StatSheet baseStats;

    public StatSheet BaseStats { get => baseStats; }
}
