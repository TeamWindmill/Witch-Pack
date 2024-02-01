using System.Collections;
using UnityEngine;

public class StatusEffect
{
    //inherit with buffs or debuffs. -> inherit again to create specific effects
    protected Effectable host;//the unit this ss is on
    protected float counter;//how long until the duration is over
    protected float duration;//ss lifetime
    private float amount;//the amount to change each stat by (might be flat or %)
    private StatType _statType;//the stats to affect
    private StatusEffectProcess process;
    private StatusEffectType statusEffectType;

    public Effectable Host { get => host; }
    public float Counter { get => counter; }
    public float Duration { get => duration; }
    public StatType StatType { get => _statType; }
    public StatusEffectProcess Process { get => process; }
    public StatusEffectType StatusEffectType { get => statusEffectType; }

    public StatusEffect(Effectable host, float duration, float amount, StatType effectedStatType, StatusEffectProcess process, StatusEffectType statusEffectType)
    {
        this.host = host;
        this.duration = duration;
        this.amount = amount;
        this._statType = effectedStatType;
        this.process = process;
        this.statusEffectType = statusEffectType;
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
        counter = 0;
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
        int amountToChange = Mathf.RoundToInt(amount / duration);
        while (counter < duration)
        {
            host.Owner.Stats.AddValueToStat(StatType, amountToChange);
            yield return new WaitForSeconds(1f);
            counter++;
        }
        host.Owner.Stats.AddValueToStat(StatType, -Mathf.RoundToInt(amount));
        Remove();
    }
    private IEnumerator InstantEffect()
    {
        host.Owner.Stats.AddValueToStat(StatType, Mathf.RoundToInt(amount));
        while (counter < duration)
        {
            yield return new WaitForSeconds(1f);
            counter++;
        }
        host.Owner.Stats.AddValueToStat(StatType, -Mathf.RoundToInt(amount));
        Remove();
    }
}
