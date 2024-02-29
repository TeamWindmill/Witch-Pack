using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitTargetHelper<T> where T: BaseUnit
{
    private BaseUnit owner;

    public event Action<BaseUnit> OnTarget;

    private List<T> targets;

    public UnitTargetHelper(Targeter<T> targeter, BaseUnit givenOwner)
    {
        owner = givenOwner;
        targets = targeter.AvailableTargets;
        
    }


    public T GetTarget(TargetData givenData, List<T> targetsToAvoid = null)
    {
        return GetTarget(targets, givenData, targetsToAvoid);
    }

    public T GetTarget(List<T> targets, TargetData givenData, List<T> targetsToAvoid = null)
    {
        if (targets.Count <= 0)
        {
            return null;
        }
        T target;
        switch (givenData.Priority)
        {
            case TargetPriority.Stat:
                target = GetTargetByStat(targets, givenData.StatType, givenData.Modifier, targetsToAvoid);
                break;
            case TargetPriority.Distance:
                target = GetTargetByDistance(targets, givenData.Modifier, targetsToAvoid);
                break;
            case TargetPriority.Random:
                target = targets[UnityEngine.Random.Range(0, targets.Count)];
                break;
            case TargetPriority.DistnaceToCore:
                target = GetTargetByDistanceToCore(targets, givenData.Modifier, targetsToAvoid);
                break;
            case TargetPriority.Threatened:
                target = GetTargetByThreat(targets, givenData.Modifier, targetsToAvoid);
                break;
            case TargetPriority.CurrentHP:
                target = GetTargetByHP(targets, givenData.Modifier, targetsToAvoid);
                break;
            case TargetPriority.CurrentHPPrecentage:
                target = GetTargetByHPPrecentage(targets, givenData.Modifier, targetsToAvoid);
                break;

            //add threat when Im doen adding the system
            default:
                return targets[0];
        }
        if(!ReferenceEquals(target,null)) OnTarget?.Invoke(target);
        return target;
    }

    private T GetTargetByThreat(List<T> targets, TargetModifier mod, List<T> targetsToAvoid)
    {
        T cur = null;
        for (int i = 0; i < targets.Count; i++)
        {
            if (!ReferenceEquals(targetsToAvoid, null) && targets.Count > targetsToAvoid.Count && targetsToAvoid.Contains(targets[i]))
            {
                continue;
            }
            if(this.targets[i].IsDead) continue;

            if (targets[i].Stats.ThreatLevel <= 0) continue;
            cur = targets[i];
            
            if (mod == TargetModifier.Most)
            {
                if (cur.Stats.ThreatLevel < targets[i].Stats.ThreatLevel)
                {
                    cur = targets[i];
                }
            }
            else
            {
                if (cur.Stats.ThreatLevel > targets[i].Stats.ThreatLevel)
                {
                    cur = targets[i];
                } 
            }
        }
        return cur;
    }
    private T GetTargetByHP(List<T> targets, TargetModifier mod, List<T> targetsToAvoid = null)
    {
        T cur = targets[targets.Count / 2];
        for (int i = 0; i < targets.Count; i++)
        {
            if(this.targets[i].IsDead) continue;

            if (mod == TargetModifier.Most)
            {
                if (cur.Damageable.CurrentHp < targets[i].Damageable.CurrentHp)
                {
                    cur = targets[i];
                }
            }
            else
            {
                if (cur.Damageable.CurrentHp > targets[i].Damageable.CurrentHp)
                {
                    cur = targets[i];
                }
            }
        }
        return cur;
    }
    private T GetTargetByHPPrecentage(List<T> targets, TargetModifier mod, List<T> targetsToAvoid = null)
    {
        T cur = targets[targets.Count / 2];
        for (int i = 0; i < targets.Count; i++)
        {
            if(this.targets[i].IsDead) continue;

            if (mod == TargetModifier.Most)
            {
                if (cur.Damageable.CurrentHp/ cur.Stats.GetStatValue(StatType.MaxHp) < targets[i].Damageable.CurrentHp / targets[i].Stats.GetStatValue(StatType.MaxHp))
                {
                    cur = targets[i];
                }
            }
            else
            {
                if (cur.Damageable.CurrentHp / cur.Stats.GetStatValue(StatType.MaxHp) > targets[i].Damageable.CurrentHp / targets[i].Stats.GetStatValue(StatType.MaxHp))
                {
                    cur = targets[i];
                }
            }
        }
        return cur;
    }
    private T GetTargetByStat(List<T> targets, StatType givenStat, TargetModifier mod, List<T> targetsToAvoid = null)
    {
        T cur = targets[targets.Count / 2];
        for (int i = 0; i < targets.Count; i++)
        {
            if(this.targets[i].IsDead) continue;

            if (mod == TargetModifier.Most)
            {
                if (cur.Stats.GetStatValue(givenStat) < targets[i].Stats.GetStatValue(givenStat))
                {
                    cur = targets[i];
                }
            }
            else
            {
                if (cur.Stats.GetStatValue(givenStat) > targets[i].Stats.GetStatValue(givenStat))
                {
                    cur = targets[i];
                }
            }
        }
        return cur;
    }

    private T GetTargetByDistance(List<T> targets, TargetModifier mod, List<T> targetsToAvoid = null)
    {
        T cur = targets[targets.Count / 2];
        for (int i = 0; i < targets.Count; i++)
        {
            if (!ReferenceEquals(targetsToAvoid, null) && targets.Count > targetsToAvoid.Count && targetsToAvoid.Contains(targets[i]))
            {
                continue;
            }
            
            if(this.targets[i].IsDead) continue;


            if (mod == TargetModifier.Most)
            {
                if (Vector3.Distance(cur.transform.position, owner.transform.position) < Vector3.Distance(targets[i].transform.position, owner.transform.position))
                {
                    cur = targets[i];
                }
            }
            else
            {
                if (Vector3.Distance(cur.transform.position, owner.transform.position) > Vector3.Distance(targets[i].transform.position, owner.transform.position))
                {
                    cur = targets[i];
                }
            }
        }
        return cur;
    }

    private T GetTargetByDistanceToCore(List<T> targets, TargetModifier mod, List<T> targetsToAvoid = null)
    {
        T cur = targets[targets.Count / 2];
        for (int i = 0; i < targets.Count; i++)
        {
            if (!ReferenceEquals(targetsToAvoid, null) && targets.Count > targetsToAvoid.Count && targetsToAvoid.Contains(targets[i]))
            {
                continue;
            }
            
            if(this.targets[i].IsDead) continue;


            if (mod == TargetModifier.Most)
            {
                if (Vector3.Distance(cur.transform.position, LevelManager.Instance.CurrentLevel.CoreTemple.transform.position) < Vector3.Distance(targets[i].transform.position, LevelManager.Instance.CurrentLevel.CoreTemple.transform.position))
                {
                    cur = targets[i];
                }
            }
            else
            {
                if (Vector3.Distance(cur.transform.position, LevelManager.Instance.CurrentLevel.CoreTemple.transform.position) > Vector3.Distance(targets[i].transform.position, LevelManager.Instance.CurrentLevel.CoreTemple.transform.position))
                {
                    cur = targets[i];
                }
            }
        }
        return cur;
    }



}
