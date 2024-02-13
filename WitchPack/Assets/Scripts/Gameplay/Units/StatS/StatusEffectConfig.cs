using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectConfig", menuName = "StatusEffect")]
public class StatusEffectConfig : ScriptableObject
{
    [SerializeField] private float duration;//every status effects only works for a duration
    [SerializeField] private int amount;//this might be useless 
    [SerializeField, Tooltip("Instant means a stat will change by the amount given for a duration then return to normal, " +
        "Over Time means a stat will decrease every 1 second by a fixed amount until reaching the given amount 1 second before the duration ends")]
    private StatusEffectProcess process;
    [SerializeField] private StatType statTypeAffected;//all stats you wish to affect on the target
    [SerializeField] private StatusEffectType statusEffectType;
    [SerializeField] private StatusEffectValueType valueType;

    public float Duration { get => duration; }
    public int Amount { get => amount; }
    public StatusEffectProcess Process { get => process; }
    public StatType StatTypeAffected { get => statTypeAffected; }
    public StatusEffectType StatusEffectType { get => statusEffectType; }
    public StatusEffectValueType ValueType { get => valueType; }
}

public enum StatusEffectProcess
{
    Instant, //change a value instantly for a duration
    OverTime//change value over a duration, every second the value will decrease or increase by a fixed amount
}

public enum StatusEffectValueType
{
    Flat,
    Percentage
}

public enum StatusEffectType
{
    None,
    Root,
    Slow,
    Charm,
    Poison,
    Hidden,
}
