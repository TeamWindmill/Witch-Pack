using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitTargetHelper
{
    private BaseUnit owner;

    public event Action<BaseUnit> OnTarget;

    public UnitTargetHelper(BaseUnit givenOwner)
    {
        owner = givenOwner;
    }


    public BaseUnit GetTarget(List<BaseUnit> targets, TargetData givenData, StatType stat = StatType.AttackSpeed /*only send in a stat if necessary*/)
    {
        if (targets.Count <= 0)
        {
            return null;
        }
        BaseUnit target;
        switch (givenData.Prio)
        {
            case TargetPrio.Stat:
                target = GetTargetByStat(targets, stat, givenData.Mod);
                break;
            case TargetPrio.Distance:
                target =  GetTargetByDistance(targets, givenData.Mod);
                break;
            case TargetPrio.Random:
                target = targets[UnityEngine.Random.Range(0, targets.Count)];
                break;
                //add threat when Im doen adding the system
            default:
                return targets[0];
        }
        OnTarget?.Invoke(target);
        return target;
    }


    private BaseUnit GetTargetByStat(List<BaseUnit> targets, StatType givenStat, TargetMod mod)
    {
        BaseUnit cur = targets[0];
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

    private BaseUnit GetTargetByDistance(List<BaseUnit> targets, TargetMod mod)
    {
        BaseUnit cur = targets[0];
        for (int i = 0; i < targets.Count; i++)
        {
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


}
