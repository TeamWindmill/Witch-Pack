using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectConfig", menuName = "StatusEffect")]
public class StatusEffectConfig : ScriptableObject
{
    [SerializeField, Tooltip("Instant means a stat will change by the amount given for a duration then return to normal, " +
        "Over Time means a stat will decrease every 1 second by a fixed amount until reaching the given amount 1 second before the duration ends")]
    private StatusEffectProcess process;
    [SerializeField, ShowIf(nameof(hasDuration))] private float duration;//every status effects only works for a duration

    
    [SerializeField] private bool multipleStats;
    //Single Stat
    [SerializeField,HideIf(nameof(multipleStats))] private float amount; 
    [SerializeField,HideIf(nameof(multipleStats))] private StatType statTypeAffected;//all stats you wish to affect on the target
    [SerializeField,HideIf(nameof(multipleStats))] private Factor factor;
    
    //Multiple Stats  
    [SerializeField,ShowIf(nameof(multipleStats))] private StatUpgrade[] statUpgrades; 
    
    [SerializeField] private StatusEffectVisual _statusEffectVisual;
    [SerializeField] private bool _showStatusEffectPopup;


    private bool hasDuration => process != StatusEffectProcess.InstantWithoutDuration;

    public float Duration => duration;
    public float Amount => amount;
    public StatusEffectProcess Process => process;
    public StatType StatTypeAffected => statTypeAffected;
    public StatusEffectVisual StatusEffectVisual => _statusEffectVisual;
    public Factor Factor => factor;
    public bool ShowStatusEffectPopup => _showStatusEffectPopup;
    public bool MultipleStats => multipleStats;
    public StatUpgrade[] StatUpgrades => statUpgrades;
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
    RockMonolith,
    Taunt,
}
