using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AoeStatPassive", menuName = "Ability/Passive/AoeStat")]
public class AoeStatPassive : Passive
{
    [SerializeField] private StatValue[] statIncreases;
    [SerializeField] private bool _affectsEnemies;
    [SerializeField] private bool _affectsShamans;

    public override void SubscribePassive(BaseUnit owner)
    {
        owner.ShamanTargeter.OnTargetAdded += AddStatToShaman;
        owner.EnemyTargeter.OnTargetAdded += AddStatToEnemy;
        owner.ShamanTargeter.OnTargetLost += RemoveStatFromShaman;
        owner.EnemyTargeter.OnTargetLost += RemoveStatFromEnemy;
    }
    protected virtual void AddStatToShaman(Shaman shaman)
    {
        if (!_affectsShamans) return;
        foreach (StatValue increase in statIncreases)
        {
            shaman.Stats.AddValueToStat(increase.Stat, increase.Value);
        }
    }
    protected virtual void RemoveStatFromShaman(Shaman shaman)
    {
        if (!_affectsShamans) return;
        foreach (StatValue increase in statIncreases)
        {
            shaman.Stats.AddValueToStat(increase.Stat, -increase.Value);
        }
    }
    protected virtual void AddStatToEnemy(Enemy enemy)
    {
        if (!_affectsEnemies) return;
        foreach (StatValue increase in statIncreases)
        {
            enemy.Stats.AddValueToStat(increase.Stat, increase.Value);
        }
    }
    protected virtual void RemoveStatFromEnemy(Enemy enemy)
    {
        if (!_affectsEnemies) return;
        foreach (StatValue increase in statIncreases)
        {
            enemy.Stats.AddValueToStat(increase.Stat, -increase.Value);
        }
    }
   
}