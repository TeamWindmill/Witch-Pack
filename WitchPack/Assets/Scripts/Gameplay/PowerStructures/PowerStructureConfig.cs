using System;
using UnityEngine;


[CreateAssetMenu(fileName = "PowerStructureConfig", menuName = "Configs/PowerStructure", order = 0)]
public class PowerStructureConfig : ScriptableObject
{
    [Range(0, 50)] public float Range;
    [Range(0, 1)] public float[] RingsRanges;
    public Color RingDefaultColor;
    public Color PowerStructureTypeColor;
    public float DefaultSpriteAlpha;
    public float SpriteAlphaFade;
    public Sprite PowerStructureSprite;
    public PowerStructureStatEffect statEffect;
    public bool ShowPercent;

    private void OnValidate()
    {
        // if (statEffect.RingValues.Length != RingsRanges.Length)
        //     statEffect.RingValues = new int[RingsRanges.Length];
    }
}

[Serializable]
public struct PowerStructureStatEffect
{
    public StatType StatType;
    public Modifier Modifier;
    public float[] RingValues;
}

public enum Modifier
{
    Addition,
    Multiplication
}