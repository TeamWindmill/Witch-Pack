using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectConfig", menuName = "StatusEffect")]
public class StatusEffectConfig : ScriptableObject
{
    [SerializeField] private float amount; 
    [SerializeField, Tooltip("Instant means a stat will change by the amount given for a duration then return to normal, " +
        "Over Time means a stat will decrease every 1 second by a fixed amount until reaching the given amount 1 second before the duration ends")]
    private StatusEffectProcess process;
    [SerializeField, ShowIf(nameof(hasDuration))] private float duration;//every status effects only works for a duration
    [SerializeField] private StatType statTypeAffected;//all stats you wish to affect on the target
    [SerializeField] private StatusEffectVisual _statusEffectVisual;
    [SerializeField] private bool _showStatusEffectPopup;
    [SerializeField] private Factor factor;
    private bool hasDuration => process != StatusEffectProcess.InstantWithoutDuration;

    public float Duration { get => duration; }
    public float Amount { get => amount; }
    public StatusEffectProcess Process { get => process; }
    public StatType StatTypeAffected { get => statTypeAffected; }
    public StatusEffectVisual StatusEffectVisual { get => _statusEffectVisual; }
    public Factor Factor => factor;
    public bool ShowStatusEffectPopup => _showStatusEffectPopup;
}

public enum StatusEffectProcess
{
    InstantWithDuration, //change a value instantly for a duration
    OverTime,//change value over a duration, every second the value will decrease or increase by a fixed amount
    InstantWithoutDuration,
}

public enum StatusEffectVisual
{
    None,
    Root,
    LongerRoot,
    PoisonRoot,
    HealingRoot,
    Slow,
    Charm,
    Hidden,
    Inspired,
    AttackSpeedBoost,
    HealthRegen,
    MovementSpeed,
    PermanentMaxHealthBonus,
    Frenzy,
    Weak,
    RockMonolithTaunt,
}
