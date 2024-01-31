using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatPassive", menuName = "Ability/Passive/Stat")]
public class StatPassive : Passive
{

    private BaseStat[] statIncreases;

    public override void SubscribePassive(BaseUnit owner)
    {
        foreach (BaseStat increase in statIncreases)
        {
            owner.Stats.AddValueToStat(increase.statType, increase.value);
        }
    }

    public override void UnsubscribePassive(BaseUnit owner)
    {
        foreach (BaseStat increase in statIncreases)
        {
            owner.Stats.AddValueToStat(increase.statType, -increase.value);
        }
    }

}
