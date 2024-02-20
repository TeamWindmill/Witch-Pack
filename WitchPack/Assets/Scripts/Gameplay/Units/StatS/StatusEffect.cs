using System.Collections;
using UnityEngine;

public class StatusEffect
{
    //inherit with buffs or debuffs. -> inherit again to create specific effects
    protected Effectable host;//the unit this ss is on
    protected float timeCounter;//how long until the duration is over
    protected float duration;//ss lifetime
    private float amount;//the amount to change each stat by (might be flat or %)
    private StatType _statType;//the stats to affect
    private StatusEffectProcess process;
    private StatusEffectType statusEffectType;
    private StatusEffectValueType statusEffectValueType;
    private float _statValue;

    public Effectable Host { get => host; }
    public float Counter { get => timeCounter; }
    public float Duration { get => duration; }
    public StatType StatType { get => _statType; }
    public StatusEffectProcess Process { get => process; }
    public StatusEffectType StatusEffectType { get => statusEffectType; }
    public StatusEffectValueType StatusEffectValueType { get => statusEffectValueType; }

    public StatusEffect(Effectable host, float duration, float amount, StatType effectedStatType, StatusEffectProcess process, StatusEffectType statusEffectType, StatusEffectValueType valueType)
    {
        this.host = host;
        this.duration = duration;
        this.amount = amount;
        this._statType = effectedStatType;
        this.process = process;
        this.statusEffectType = statusEffectType;
        this.statusEffectValueType = valueType;
    }


    public virtual void Activate()
    {
        switch (process)
        {
            case StatusEffectProcess.InstantWithDuration:
                host.Owner.StartCoroutine(InstantEffect());
                break;
            case StatusEffectProcess.OverTime:
                host.Owner.StartCoroutine(OverTimeEffect());
                break;
            case StatusEffectProcess.InstantWithoutDuration:
                InstantEffectWithoutDuration();
                break;
        }
    }

    public virtual void Remove()
    {
        host.RemoveEffect(this);
        host?.Owner.Stats.AddValueToStat(StatType, -_statValue);
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
                _statValue = Mathf.RoundToInt(amount);
                break;
            case StatusEffectValueType.Percentage:
                _statValue = Mathf.RoundToInt((amount / 100) * host.Owner.Stats.GetStatValue(_statType));
                break;
            case StatusEffectValueType.FlatToFloat:
                _statValue = amount;
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
                _statValue = Mathf.RoundToInt(amount / duration);
                break;
            case StatusEffectValueType.Percentage:
                _statValue = Mathf.RoundToInt((amount / 100) * host.Owner.Stats.GetStatValue(_statType)) / duration;
                break;
            case StatusEffectValueType.FlatToFloat:
                _statValue = amount / duration;
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
                _statValue = Mathf.RoundToInt(amount);
                break;
            case StatusEffectValueType.Percentage:
                _statValue = Mathf.RoundToInt((amount / 100) * host.Owner.Stats.GetStatValue(_statType));
                break;
            case StatusEffectValueType.FlatToFloat:
                _statValue = amount;
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
