using System.Collections.Generic;
using UnityEngine;

public class MultiShot : OffensiveAbility
{
    protected Enemy[] _targets;
    public readonly MultiShotSO MultishotConfig;
    
    public MultiShot(MultiShotSO config, BaseUnit owner) : base(config, owner)
    {
        MultishotConfig = config;
        abilityStats.Add(new AbilityStat(AbilityStatType.ProjectilesAmount,config.ProjectilesAmount));
    }

    public override bool CastAbility(out IDamagable target)
    {
        var projectilesAmount = (int)GetAbilityStatValue(AbilityStatType.ProjectilesAmount);

        _targets = new Enemy[projectilesAmount];
        var targetsToIgnore = new List<Enemy>();
        
        //get an initial target
        _targets[0] = Owner.EnemyTargetHelper.GetTarget(TargetData);
        if (!ReferenceEquals(_targets[0], null))
        {
            targetsToIgnore.Add(_targets[0]);
            
            for (int i = 1; i < _targets.Length; i++)
            {
                _targets[i] = Owner.EnemyTargetHelper.GetTarget(TargetData, targetsToIgnore);
                _targets[i] ??= _targets[0];
                targetsToIgnore.Add(_targets[i]);
            }
            
            //calculate start direction
            var dir = _targets[0].transform.position - Owner.transform.position;
            var dirAngle = Vector3.SignedAngle(Vector3.up, dir.normalized,Vector3.forward);
            for (int i = 0; i < projectilesAmount; i++)
            {
                var offset = MultishotConfig.Offset;
                var shotMono = GetPooledObject();
                shotMono.transform.position = Owner.CastPos.position;
                shotMono.gameObject.SetActive(true);
                
                if (projectilesAmount % 2 == 0)
                {
                    if (i <= 1) offset /= 2;
                    if (i % 2 != 0)
                    {
                        shotMono.Init(MultishotConfig.MultiShotType,Owner, _targets[i], this, dirAngle + offset * (i/2+1));
                    }
                    else
                    {
                        shotMono.Init(MultishotConfig.MultiShotType,Owner, _targets[i], this, dirAngle - offset * (i/2+1));
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        shotMono.Init(MultishotConfig.MultiShotType,Owner, _targets[i], this, dirAngle);
                    }
                    else if (i % 2 == 0)
                    {
                        shotMono.Init(MultishotConfig.MultiShotType,Owner, _targets[i], this, dirAngle + MultishotConfig.Offset * (i/2));
                    }
                    else
                    {
                        shotMono.Init(MultishotConfig.MultiShotType,Owner, _targets[i], this, dirAngle - MultishotConfig.Offset * ((i+1)/2));
                    } 
                }
            }

            target = _targets[0];
            return true;
        }

        target = null;
        return false;
    }

    public override bool CheckCastAvailable()
    {
        var target = Owner.EnemyTargetHelper.GetTarget(TargetData);
        if (target != null) return true;

        return false;
    }
    
    protected virtual MultiShotMono GetPooledObject()
    {
        return LevelManager.Instance.PoolManager.MultiShotPool.GetPooledObject();
    }
}