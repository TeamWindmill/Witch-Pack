using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Courage", menuName = "Ability/Passive/JavanCourage")]
public class AffectedByUnitsStatPassive : Passive
{
    [Header("Affected By Units Stat Passive")]
    [SerializeField] private StatValue[] _statsIncrease;
    [SerializeField] private bool _affectedByEnemies;
    [SerializeField][ShowIf(nameof(_affectedByEnemies))] private bool _affectedByEnemiesWithStatusEffect;
    [SerializeField][ShowIf(nameof(_affectedByEnemiesWithStatusEffect))] private StatusEffectType _enemyStatusEffect;
    [SerializeField] private bool _affectedByShamans;
    [SerializeField][ShowIf(nameof(_affectedByShamans))] private bool _affectedByShamansWithStatusEffect;
    [SerializeField][ShowIf(nameof(_affectedByShamansWithStatusEffect))] private StatusEffectType _shamanStatusEffect;


    private BaseUnit _owner;
    public override void SubscribePassive(BaseUnit owner)
    {
        _owner = owner;
        if (_affectedByEnemies) owner.EnemyTargeter.OnTargetAdded += enemy => ChangeStatByEnemy(enemy,true);
        if (_affectedByEnemies) owner.EnemyTargeter.OnTargetLost += enemy => ChangeStatByEnemy(enemy,false);
        if (_affectedByShamans) owner.ShamanTargeter.OnTargetAdded += shaman => ChangeStatByShaman(shaman,true);
        if (_affectedByShamans) owner.ShamanTargeter.OnTargetLost += shaman => ChangeStatByShaman(shaman,false);
    }

    private void ChangeStatByEnemy(Enemy enemy,bool addition)
    {
        if (_affectedByEnemiesWithStatusEffect)
        {
            if (enemy.Effectable.ContainsStatusEffect(_enemyStatusEffect))
            {
                foreach (var stat in _statsIncrease)
                {
                    if(addition) _owner.Stats.AddValueToStat(stat.StatType,stat.Value);
                    else _owner.Stats.AddValueToStat(stat.StatType,-stat.Value);
                }
            }
        }
        else
        {
            foreach (var stat in _statsIncrease)
            {
                if(addition) _owner.Stats.AddValueToStat(stat.StatType,stat.Value);
                else _owner.Stats.AddValueToStat(stat.StatType,-stat.Value);
            }
        }
    }
    private void ChangeStatByShaman(Shaman shaman, bool addition)
    {
        if (_affectedByShamansWithStatusEffect)
        {
            if (shaman.Effectable.ContainsStatusEffect(_shamanStatusEffect))
            {
                foreach (var stat in _statsIncrease)
                {
                    if(addition) _owner.Stats.AddValueToStat(stat.StatType,stat.Value);
                    else _owner.Stats.AddValueToStat(stat.StatType,-stat.Value);
                }
            }
        }
        else
        {
            foreach (var stat in _statsIncrease)
            {
                if(addition) _owner.Stats.AddValueToStat(stat.StatType,stat.Value);
                else _owner.Stats.AddValueToStat(stat.StatType,-stat.Value);
            }
        }
    }
}