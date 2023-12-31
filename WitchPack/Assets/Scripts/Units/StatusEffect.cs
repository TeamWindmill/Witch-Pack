using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect
{
    //inherit with buffs or debuffs. -> inherit again to create specific effects
    protected Effectable host;
    protected float counter;
    protected float duration;

    public Effectable Host { get => host; }
    public float Counter { get => counter; }
    public float Duration { get => duration; }

    public void CacheHost(Effectable givenHost)
    {
        host = givenHost;
    }

    public virtual void Activate()
    {
        Subscribe();
    }

    public virtual void Remove()
    {
        UnSubscribe();
    }

    public virtual void Reset()
    {

    }

    protected virtual void Subscribe()
    {

    }
    protected virtual void UnSubscribe()
    {

    }


}
