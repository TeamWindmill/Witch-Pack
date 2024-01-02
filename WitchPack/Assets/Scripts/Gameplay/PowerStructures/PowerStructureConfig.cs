using UnityEngine;


[CreateAssetMenu(fileName = "PowerStructureConfig", menuName = "Configs/PowerStructure", order = 0)]
public class PowerStructureConfig : ScriptableObject
{
    [Range(0, 20)] public float Range;
    [Range(0, 1)] public float[] RingsRanges;
    public Color RingDefaultColor;
    public Color PowerStructureTypeColor;
    public float DefaultSpriteAlpha;
    public float SpriteAlphaFade;
    //public StatEffectConfig StatEffectConfig;
    public Sprite PowerStructureSprite;

    private void OnValidate()
    {
        // if (StatEffectConfig.StatModifier.RingModifiers.Length != RingsRanges.Length)
        //     StatEffectConfig.StatModifier.ChangeRingModifiersNumber(RingsRanges.Length);
    }
}