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


    public T GetTarget(TargetData givenData, List<T> targetsToAvoid = null, StatType stat = StatType.AttackSpeed)
    {
        return GetTarget(targets, givenData, targetsToAvoid, stat);
    }

    public T GetTarget(List<T> targets, TargetData givenData, List<T> targetsToAvoid = null, StatType stat = StatType.AttackSpeed)
    {
        if (targets.Count <= 0)
        {
            return null;
        }
        T target;
        switch (givenData.Prio)
        {
            case TargetPrio.Stat:
                target = GetTargetByStat(targets, stat, givenData.Mod, targetsToAvoid);
                break;
            case TargetPrio.Distance:
                target = GetTargetByDistance(targets, givenData.Mod, targetsToAvoid);
                break;
            case TargetPrio.Random:
                target = targets[UnityEngine.Random.Range(0, targets.Count)];
                break;
            case TargetPrio.DistnaceToCore:
                target = GetTargetByDistanceToCore(targets, givenData.Mod, targetsToAvoid);
                break;
            case TargetPrio.Threatened:
                target = GetTargetByThreat(targets, givenData.Mod, targetsToAvoid);
                break;

            //add threat when Im doen adding the system
            default:
                return targets[0];
        }
        if(!ReferenceEquals(target,null)) OnTarget?.Invoke(target);
        return target;
    }

    private T GetTargetByThreat(List<T> targets, TargetMod mod, List<T> targetsToAvoid)
    {
        T cur = null;
        for (int i = 0; i < targets.Count; i++)
        {
            if (!ReferenceEquals(targetsToAvoid, null) && targets.Count > targetsToAvoid.Count && targetsToAvoid.Contains(targets[i]))
            {
                continue;
            }

            if (targets[i].Stats.ThreatLevel <= 0) continue;
            else cur = targets[i];
            
            if (mod == TargetMod.Most)
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

    private T GetTargetByStat(List<T> targets, StatType givenStat, TargetMod mod, List<T> targetsToAvoid = null)
    {
        T cur = targets[targets.Count / 2];
        for (int i = 0; i < targets.Count; i++)
        {
            if (mod == TargetMod.Most)
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

    private T GetTargetByDistance(List<T> targets, TargetMod mod, List<T> targetsToAvoid = null)
    {
        T cur = targets[targets.Count / 2];
        for (int i = 0; i < targets.Count; i++)
        {
            if (!ReferenceEquals(targetsToAvoid, null) && targets.Count > targetsToAvoid.Count && targetsToAvoid.Contains(targets[i]))
            {
                continue;
            }

            if (mod == TargetMod.Most)
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

    private T GetTargetByDistanceToCore(List<T> targets, TargetMod mod, List<T> targetsToAvoid = null)
    {
        T cur = targets[targets.Count / 2];
        for (int i = 0; i < targets.Count; i++)
        {
            if (!ReferenceEquals(targetsToAvoid, null) && targets.Count > targetsToAvoid.Count && targetsToAvoid.Contains(targets[i]))
            {
                continue;
            }

            if (mod == TargetMod.Most)
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
