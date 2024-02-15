using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectTypeColorDictionary", menuName = "StatusEffectTypeColorDictionary")]
public class StatusEffectTypeColorDictionary : ScriptableObject
{
    public List<StatusEffectTypeColorPair> StatusEffectTypeColorList;

    public Color GetColorByStatusEffectType(StatusEffectType givenType)
    {
        foreach (StatusEffectTypeColorPair pair in StatusEffectTypeColorList)
        {
            
            if(pair.StatusEffectType == givenType)
            {
                return pair.Color;
            }
        }

        throw new Exception("Status Effect Type Not Found");
    }

    public StatusEffectType GetStatusEffectTypeByColor(Color givenColor)
    {
        foreach (StatusEffectTypeColorPair pair in StatusEffectTypeColorList)
        {
            if (pair.Color == givenColor)
            {
                return pair.StatusEffectType;
            }
        }

        throw new Exception("Color Not Found");
    }
}


[Serializable]
public class StatusEffectTypeColorPair
{
    [SerializeField] private StatusEffectType statusEffectType;
    [SerializeField] private Color color;

    public StatusEffectType StatusEffectType { get => statusEffectType; }
    public Color Color { get => color; }
}