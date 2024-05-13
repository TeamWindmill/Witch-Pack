using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PopupsColorsDictionary", menuName = "PopupsColorsDictionary")]
public class PopupsColorsDictionary : ScriptableObject
{
    [SerializeField, TabGroup("Numbers")] private Color defaultColor;
    [SerializeField, TabGroup("Numbers")] private Color shamanCritColor;
    [SerializeField, TabGroup("Numbers")] private Color enemyAutoAttackColor;
    [SerializeField, TabGroup("Numbers")] private Color enemyCritAutoAttackColor;
    [SerializeField, TabGroup("Numbers")] private Color healColor;

    [SerializeField, TabGroup("Status Effects")] private List<StatusEffectTypeVisualData> statusEffectsVisuals;

    public Color DefaultColor { get => defaultColor; }
    public Color ShamanCritColor { get => shamanCritColor; }
    public Color EnemyAutoAttackColor { get => enemyAutoAttackColor; }
    public Color EnemyCritAutoAttackColor { get => enemyCritAutoAttackColor; }
    public Color HealColor { get => healColor; }

    public StatusEffectTypeVisualData GetData(StatusEffectType givenType)
    {
        foreach (StatusEffectTypeVisualData data in statusEffectsVisuals)
        {

            if (data.StatusEffectType == givenType)
            {
                return data;
            }
        }

        throw new Exception("Status Effect Type Not Found");
    }

    public StatusEffectTypeVisualData GetData(Color givenColor)
    {
        foreach (StatusEffectTypeVisualData data in statusEffectsVisuals)
        {
            if (data.Color == givenColor)
            {
                return data;
            }
        }

        throw new Exception("Color Not Found");
    }
}


[Serializable]
public class StatusEffectTypeVisualData
{
    [SerializeField] private StatusEffectType statusEffectType;
    [SerializeField] private Color color;
    [SerializeField] private string name;

    public StatusEffectType StatusEffectType { get => statusEffectType; }
    public Color Color { get => color; }
    public string Name { get => name; }
}