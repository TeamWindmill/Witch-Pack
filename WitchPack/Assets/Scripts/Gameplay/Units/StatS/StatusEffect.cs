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
    private bool shouldReturnToNormalAtEndOfDuration;

    public Effectable Host { get => host; }
    public float Counter { get => timeCounter; }
    public float Duration { get => duration; }
    public StatType StatType { get => _statType; }
    public StatusEffectProcess Process { get => process; }
    public StatusEffectType StatusEffectType { get => statusEffectType; }
    public StatusEffectValueType StatusEffectValueType { get => statusEffectValueType; }

    public StatusEffect(Effectable host, float duration, float amount, StatType effectedStatType, StatusEffectProcess process, StatusEffectType statusEffectType, StatusEffectValueType valueType, bool shouldReturnToNormalAtEndOfDuration)
    {
        this.host = host;
        this.duration = duration;
        this.amount = amount;
        this._statType = effectedStatType;
        this.process = process;
        this.statusEffectType = statusEffectType;
        this.statusEffectValueType = valueType;
        this.shouldReturnToNormalAtEndOfDuration = shouldReturnToNormalAtEndOfDuration;
    }


    public virtual void Activate()
    {
        Subscribe();
    }

    public virtual void Remove()
    {
        UnSubscribe();
        host.RemoveEffect(this);
    }

    public virtual void Reset()
    {
        timeCounter = 0;
    }

    protected virtual void Subscribe()
    {
        switch (process)
        {
            case StatusEffectProcess.Instant:
                host.Owner.StartCoroutine(InstantEffect());
                break;
            case StatusEffectProcess.OverTime:
                host.Owner.StartCoroutine(OverTimeEffect());
                break;
        }
    }
    protected virtual void UnSubscribe()
    {

    }


    private IEnumerator OverTimeEffect()
    {
        float amountToChange = 0;
        switch (statusEffectValueType)
        {
            case StatusEffectValueType.Flat:
                amountToChange = Mathf.RoundToInt(amount / duration);
                break;
            case StatusEffectValueType.Percentage:
                amountToChange = Mathf.RoundToInt((amount / 100) * host.Owner.Stats.GetStatValue(_statType)) / duration;
                break;
        }

        while (timeCounter < duration)
        {
            host.Owner.Stats.AddValueToStat(StatType, amountToChange);
            float counter = 0f;
            while (counter < 1)
            {
                counter += GAME_TIME.GameDeltaTime;
                yield return new WaitForEndOfFrame();
            }
            timeCounter++;
        }
        if(shouldReturnToNormalAtEndOfDuration)
        {
            host.Owner.Stats.AddValueToStat(StatType, -amountToChange);
        }
        Remove();
    }
    private IEnumerator InstantEffect()
    {
        float amountToChange = 0;
        switch (statusEffectValueType)
        {
            case StatusEffectValueType.Flat:
                amountToChange = amount;
                break;
            case StatusEffectValueType.Percentage:
                amountToChange = Mathf.RoundToInt((amount / 100) * host.Owner.Stats.GetStatValue(_statType));
                break;
        }
        host.Owner.Stats.AddValueToStat(StatType, amountToChange);
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
        if(shouldReturnToNormalAtEndOfDuration)
        {
            host.Owner.Stats.AddValueToStat(StatType, -amountToChange);
        }        
        Remove();
    }
}
