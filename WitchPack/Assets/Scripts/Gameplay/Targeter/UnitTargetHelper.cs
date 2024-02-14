using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitTargetHelper
{
    private BaseUnit owner;

    public event Action<BaseUnit> OnTarget;

    public UnitTargetHelper(BaseUnit givenOwner)
    {
        owner = givenOwner;
    }


    public BaseUnit GetTarget(List<BaseUnit> targets, TargetData givenData, List<BaseUnit> targetsToAvoid = null, StatType stat = StatType.AttackSpeed)
    {
        if (targets.Count <= 0)
        {
            return null;
        }
        BaseUnit target;
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
        OnTarget?.Invoke(target);
        return target;
    }

    private BaseUnit GetTargetByThreat(List<BaseUnit> targets, TargetMod mod, List<BaseUnit> targetsToAvoid)
    {
        BaseUnit cur = targets[targets.Count / 2];
        for (int i = 0; i < targets.Count; i++)
        {
            if (!ReferenceEquals(targetsToAvoid, null) && targets.Count > targetsToAvoid.Count && targetsToAvoid.Contains(targets[i]))
            {
                continue;
            }

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

    private BaseUnit GetTargetByStat(List<BaseUnit> targets, StatType givenStat, TargetMod mod, List<BaseUnit> targetsToAvoid = null)
    {
        BaseUnit cur = targets[targets.Count / 2];
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

    private BaseUnit GetTargetByDistance(List<BaseUnit> targets, TargetMod mod, List<BaseUnit> targetsToAvoid = null)
    {
        BaseUnit cur = targets[targets.Count / 2];
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

    private BaseUnit GetTargetByDistanceToCore(List<BaseUnit> targets, TargetMod mod, List<BaseUnit> targetsToAvoid = null)
    {
        BaseUnit cur = targets[targets.Count / 2];
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
