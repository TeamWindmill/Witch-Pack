using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectTypeColorDictionary", menuName = "StatusEffectTypeColorDictionary")]
public class StatusEffectTypeColorDictionary : ScriptableObject
{
    public List<StatusEffectTypeVisualData> DataList;

    public StatusEffectTypeVisualData GetData(StatusEffectVisual givenVisual)
    {
        foreach (StatusEffectTypeVisualData data in DataList)
        {
            
            if(data.StatusEffectVisual == givenVisual)
            {
                return data;
            }
        }

        throw new Exception("Status Effect Type Not Found");
    }

    public StatusEffectTypeVisualData GetData(Color givenColor)
    {
        foreach (StatusEffectTypeVisualData data in DataList)
        {
            if (data.Color == givenColor)
            {
                return data;
            }
        }

        throw new Exception("Color Not Found");
    }
}


//[Serializable]
//public class StatusEffectTypeVisualData
//{
//    [SerializeField] private StatusEffectType statusEffectType;
//    [SerializeField] private Color color;
//    [SerializeField] private string name;

//    public StatusEffectType StatusEffectType { get => statusEffectType; }
//    public Color Color { get => color; }
//    public string Name { get => name; }
//}