using System;
using System.Collections;
using UnityEngine;

public class StatusEffect
{
    public event Action<Effectable, StatusEffect> Started;
    public event Action<Effectable, StatusEffect> Ended;

    //inherit with buffs or debuffs. -> inherit again to create specific effects
    private Effectable host; //the unit this ss is on
    private StatusEffectProcess process;
    private float duration; //ss lifetime

    private bool multipleStats;

    //single stat TODO need to remove this after all status effect configs are replaced
    //private float statValue;//the amount to change each stat by (might be flat or %)
    //private StatType _statType;//the stats to affect
    //private Factor factor;

    //multiple stats
    private StatUpgrade[] statUpgrades;

    private StatusEffectVisual _statusEffectVisual;
    private bool _showStatusEffectPopup;

    private float timeCounter; //how long until the duration is over
    public Effectable Host => host;
    public float Counter => timeCounter;

    public float Duration => duration;

    //public StatType StatType => _statType;
    public StatusEffectProcess Process => process;

    public StatusEffectVisual StatusEffectVisual => _statusEffectVisual;

    //public Factor Factor => factor;
    public bool ShowStatusEffectPopup => _showStatusEffectPopup;

    public StatusEffect(Effectable host, StatusEffectConfig config)
    {
        this.host = host;
        duration = config.Duration;
        //statValue = config.Amount;
        //_statType = config.StatTypeAffected;
        process = config.Process;
        _statusEffectVisual = config.StatusEffectVisual;
        _showStatusEffectPopup = config.ShowStatusEffectPopup;
        //factor = config.Factor;
        multipleStats = config.MultipleStats;
        statUpgrades = config.StatUpgrades;
    }

    public StatusEffect(Effectable host, StatusEffectData data)
    {
        this.host = host;
        duration = data.Duration.Value;
        //statValue = data.StatValue.Value;
        //_statType = data.StatTypeAffected;
        process = data.Process;
        _statusEffectVisual = data.StatusEffectVisual;
        _showStatusEffectPopup = data.ShowStatusEffectPopup;
        //factor = data.Factor;
        multipleStats = data.MultipleStats;
        statUpgrades = data.StatUpgrades;
    }

    public virtual void Activate()
    {
        switch (process)
        {
            case StatusEffectProcess.InstantWithDuration:
                host.Owner.GameObject.StartCoroutine(InstantEffect());
                break;
            case StatusEffectProcess.OverTime: //not working currently
                //host.Owner.GameObject.StartCoroutine(OverTimeEffect()); 
                break;
            case StatusEffectProcess.InstantWithoutDuration:
                InstantEffectWithoutDuration();
                break;
        }

        Started?.Invoke(host, this);
    }

    public virtual void Remove()
    {
        host.RemoveEffect(this);
        //host?.Owner.Stats.RemoveValueFromStat(StatType, factor, statValue);
        RemoveValueFromStat();
        Ended?.Invoke(host, this);
    }

    public virtual void Reset()
    {
        timeCounter = 0;
    }

    private void InstantEffectWithoutDuration()
    {
        AddValueToStat();
    }

    // private IEnumerator OverTimeEffect(StatUpgrade statUpgrade)
    // {
    //     statValue /= duration;
    //
    //     while (timeCounter <= duration)
    //     {
    //         AddValueToStat();
    //         //host.Owner.Stats.AddValueToStat(StatType, factor, statValue);
    //         float counter = 0f;
    //         while (counter < 1)
    //         {
    //             counter += GAME_TIME.GameDeltaTime;
    //             yield return new WaitForEndOfFrame();
    //         }
    //
    //         timeCounter++;
    //     }
    //
    //     Remove();
    // }

    private IEnumerator InstantEffect()
    {
        //host.Owner.Stats.AddValueToStat(StatType, factor, statValue);
        AddValueToStat();
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


    private void AddValueToStat()
    {
        foreach (var statUpgrade in statUpgrades)
        {
            host?.Owner.Stats[statUpgrade.StatType].AddStatValue(statUpgrade.Factor, statUpgrade.StatValue);
        }
    }

    private void RemoveValueFromStat()
    {
        foreach (var statUpgrade in statUpgrades)
        {
            host?.Owner.Stats[statUpgrade.StatType].RemoveStatValue(statUpgrade.Factor, statUpgrade.StatValue);
        }
    }
}