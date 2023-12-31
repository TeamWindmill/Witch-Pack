using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbility : ScriptableObject
{
    [SerializeField] private float cd;
    //list of status effect configs


    public float Cd { get => cd;  }

    //cd
    //mana costs etc... 
    //every executable ability in the game inherits from this SO
}
