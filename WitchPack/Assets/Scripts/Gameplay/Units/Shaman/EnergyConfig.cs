using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnergyConfig", menuName = "EnergyConfig")]
public class EnergyConfig : ScriptableObject
{
    [Header("Energy")] 
    [SerializeField,Range(0,100)] private float _assistPercent;
    [Header("Energy Points Required to Level Up")] 
    [SerializeField] private int level1;
    [SerializeField] private int level2;
    [SerializeField] private int level3;
    [SerializeField] private int level4;
    [SerializeField] private int level5;
    [SerializeField] private int level6;
    private int maxLevel;
    
    public int Level1 => level1;
    public int Level2 => level2;
    public int Level3 => level3;
    public int Level4 => level4;
    public int Level5 => level5;
    public int Level6 => level6;
    public int MaxLevel => maxLevel;
    public float AssistPercent => _assistPercent/100;
}
