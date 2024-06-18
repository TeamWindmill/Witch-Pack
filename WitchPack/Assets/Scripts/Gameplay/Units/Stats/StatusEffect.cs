using System;
using System.Collections;
using UnityEngine;

public class StatusEffect
{
    public event Action<Effectable,StatusEffect> Started;
    public event Action<Effectable,StatusEffect> Ended;
    
    //inherit with buffs or debuffs. -> inherit again to create specific effects
    protected Effectable host;//the unit this ss is on
    protected float timeCounter;//how long until the duration is over
    protected float duration;//ss lifetime
    private float statValue;//the amount to change each stat by (might be flat or %)
    private StatType _statType;//the stats to affect
    private StatusEffectProcess process;
    private StatusEffectType statusEffectType;
    private StatusEffectValueType statusEffectValueType;
    private float _statValue;
    private bool _showStatusEffectPopup;

    public Effectable Host { get => host; }
    public float Counter { get => timeCounter; }
    public float Duration { get => duration; }
    public StatType StatType { get => _statType; }
    public StatusEffectProcess Process { get => process; }
    public StatusEffectType StatusEffectType { get => statusEffectType; }
    public StatusEffectValueType StatusEffectValueType { get => statusEffectValueType; }
    public bool ShowStatusEffectPopup => _showStatusEffectPopup;

    public StatusEffect(Effectable host, StatusEffectConfig config)
    {
        this.host = host;
        duration = config.Duration;
        statValue = config.Amount;
        _statType = config.StatTypeAffected;
        process = config.Process;
        statusEffectType = config.StatusEffectType;
        statusEffectValueType = config.ValueType;
        _showStatusEffectPopup = config.ShowStatusEffectPopup;
    }
    public StatusEffect(Effectable host, StatusEffectData data)
    {
        this.host = host;
        duration = data.Duration;
        statValue = data.StatValue;
        _statType = data.StatTypeAffected;
        process = data.Process;
        statusEffectType = data.StatusEffectType;
        statusEffectValueType = data.ValueType;
        _showStatusEffectPopup = data.ShowStatusEffectPopup;
    }

    public virtual void Activate()
    {
        switch (process)
        {
            case StatusEffectProcess.InstantWithDuration:
                host.Owner.GameObject.StartCoroutine(InstantEffect());
                break;
            case StatusEffectProcess.OverTime:
                host.Owner.GameObject.StartCoroutine(OverTimeEffect());
                break;
            case StatusEffectProcess.InstantWithoutDuration:
                InstantEffectWithoutDuration();
                break;
        }
        Started?.Invoke(host,this);
    }

    public virtual void Remove()
    {
        host.RemoveEffect(this);
        host?.Owner.Stats.AddValueToStat(StatType, -_statValue);
        Ended?.Invoke(host,this);
    }
    public virtual void Reset()
    {
        timeCounter = 0;
    }

    private void InstantEffectWithoutDuration()
    {
        _statValue = 0;
        switch (statusEffectValueType)
        {
            case StatusEffectValueType.FlatToInt:
                _statValue = Mathf.RoundToInt(statValue);
                break;
            case StatusEffectValueType.Percentage:
                _statValue = (statValue / 100) * host.Owner.Stats.GetStatValue(_statType);
                break;
            case StatusEffectValueType.FlatToFloat:
                _statValue = statValue;
                break;
        }
        host.Owner.Stats.AddValueToStat(StatType, _statValue);
        
    }
    private IEnumerator OverTimeEffect()
    {
        _statValue = 0;
        switch (statusEffectValueType)
        {
            case StatusEffectValueType.FlatToInt:
                _statValue = Mathf.RoundToInt(statValue / duration);
                break;
            case StatusEffectValueType.Percentage:
                _statValue = (statValue / 100) * host.Owner.Stats.GetStatValue(_statType) / duration;
                break;
            case StatusEffectValueType.FlatToFloat:
                _statValue = statValue / duration;
                break;
        }

        while (timeCounter <= duration)
        {
            host.Owner.Stats.AddValueToStat(StatType, _statValue);
            float counter = 0f;
            while (counter < 1)
            {
                counter += GAME_TIME.GameDeltaTime;
                yield return new WaitForEndOfFrame();
            }
            timeCounter++;
        }
        Remove();
    }
    private IEnumerator InstantEffect()
    {
        _statValue = 0;
        switch (statusEffectValueType)
        {
            case StatusEffectValueType.FlatToInt:
                _statValue = Mathf.RoundToInt(statValue);
                break;
            case StatusEffectValueType.Percentage:
                _statValue = (statValue / 100) * host.Owner.Stats.GetStatValue(_statType);
                break;
            case StatusEffectValueType.FlatToFloat:
                _statValue = statValue;
                break;
        }
        host.Owner.Stats.AddValueToStat(StatType, _statValue);
        while (timeCounter < duration)
        {
            float counter = 0f;
            while (counter < 1)
            {
                counter += GAME_TIME.GameDeltaTime;
                yield return new WaitForEndOfFrame();
            }
            timeCounter++;
        }
        Remove();
    }
}

