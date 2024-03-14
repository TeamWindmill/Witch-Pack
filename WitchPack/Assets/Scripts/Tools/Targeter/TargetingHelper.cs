using System.Collections.Generic;
using UnityEngine;

namespace Tools.Targeter
{
    public static class TargetingHelper<T> where T : BaseUnit
    {
        
        /// <summary>
    /// gets a target from a list of targets according to target data
    /// for distance to work you must give an origin position
    /// </summary>
    /// <param name="targets"></param>
    /// <param name="givenData"></param>
    /// <param name="targetsToAvoid"></param>
    /// <returns></returns>
    public static T GetTarget(List<T> targets, TargetData givenData, List<T> targetsToAvoid = null, Transform originPos = null)
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
                target = GetTargetByDistance(targets, givenData.Modifier,originPos, targetsToAvoid);
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
            default:
                return targets[0];
        }
        return target;
    }

    public static T GetTargetByThreat(List<T> targets, TargetModifier mod, List<T> targetsToAvoid)
    {
        T cur = targets[targets.Count/2];
        for (int i = 0; i < targets.Count; i++)
        {
            if (!ReferenceEquals(targetsToAvoid, null)  && targetsToAvoid.Contains(targets[i]))
            {
                if (cur == targets[i]) cur = null;
                continue;
            }
            
            if(targets[i].IsDead) continue;

            if (targets[i].Stats.ThreatLevel <= 0) continue;
            
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
    public static T GetTargetByHP(List<T> targets, TargetModifier mod, List<T> targetsToAvoid = null)
    {
        T cur = targets[targets.Count/2];
        for (int i = 0; i < targets.Count; i++)
        {
            if (!ReferenceEquals(targetsToAvoid, null) && targetsToAvoid.Contains(targets[i]))
            {
                if (cur == targets[i]) cur = null;
                continue;
            }
            
            if(targets[i].IsDead) continue;
            
            if (!ReferenceEquals(cur, null))
            {
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
        }
        return cur;
    }
    public static T GetTargetByHPPrecentage(List<T> targets, TargetModifier mod, List<T> targetsToAvoid = null)
    {
        T cur = targets[targets.Count/2];
        for (int i = 0; i < targets.Count; i++)
        {
            if (!ReferenceEquals(targetsToAvoid, null) && targetsToAvoid.Contains(targets[i]))
            {
                if (cur == targets[i]) cur = null;
                continue;
            }
            
            if(targets[i].IsDead) continue;
            
            if (!ReferenceEquals(cur, null))
            {
                if (mod == TargetModifier.Most)
                {
                    if (cur.Damageable.CurrentHp / cur.Stats.GetStatValue(StatType.MaxHp) < targets[i].Damageable.CurrentHp / targets[i].Stats.GetStatValue(StatType.MaxHp))
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
        }
        return cur;
    }
    public static T GetTargetByStat(List<T> targets, StatType givenStat, TargetModifier mod, List<T> targetsToAvoid = null)
    {
        T cur = targets[targets.Count/2];
        for (int i = 0; i < targets.Count; i++)
        {
            if (!ReferenceEquals(targetsToAvoid, null) && targetsToAvoid.Contains(targets[i]))
            {
                if (cur == targets[i]) cur = null;
                continue;
            }
            
            if(targets[i].IsDead) continue;
            
            if (!ReferenceEquals(cur, null))
            {
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
        }
        return cur;
    }

    public static T GetTargetByDistance(List<T> targets, TargetModifier mod,Transform originPos, List<T> targetsToAvoid = null)
    {
        T cur = targets[targets.Count/2];
        for (int i = 0; i < targets.Count; i++)
        {
            if (!ReferenceEquals(targetsToAvoid, null)  && targetsToAvoid.Contains(targets[i]))
            {
                if (cur == targets[i]) cur = null;
                continue;
            }
            
            if(targets[i].IsDead) continue;

            if (!ReferenceEquals(cur, null))
            {
                if (mod == TargetModifier.Most)
                {
                    if (Vector3.Distance(cur.transform.position, originPos.position) < Vector3.Distance(targets[i].transform.position, originPos.position))
                    {
                        cur = targets[i];
                    }
                }
                else
                {
                    if (Vector3.Distance(cur.transform.position, originPos.position) > Vector3.Distance(targets[i].transform.position, originPos.position))
                    {
                        cur = targets[i];
                    }
                }
            }
        }
        return cur;
    }

    public static T GetTargetByDistanceToCore(List<T> targets, TargetModifier mod, List<T> targetsToAvoid = null)
    {
        T cur = targets[targets.Count/2];
        for (int i = 0; i < targets.Count; i++)
        {
            if (!ReferenceEquals(targetsToAvoid, null) && targetsToAvoid.Contains(targets[i]))
            {
                if (cur == targets[i]) cur = null;
                continue;
            }
            
            if(targets[i].IsDead) continue;

            if (!ReferenceEquals(cur, null))
            {
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
        }
        return cur;
    }
        public static List<T> GetAvailableTargets(Vector3 originPos, float range, LayerMask layer, List<T> targetsToAvoid = null)
        {
            Collider2D[] foundColldiers = Physics2D.OverlapCircleAll(originPos, range, layer);
            List<T> legalTargets = new List<T>();

            foreach (var collider in foundColldiers)
            {
                T possibleTarget = collider.GetComponent<T>();
                if (!ReferenceEquals(possibleTarget, null))
                {
                    if (targetsToAvoid != null)
                    {
                        if (targetsToAvoid.Contains(possibleTarget))
                            continue;
                    }
                        
                    legalTargets.Add(possibleTarget);
                }
            }
            return legalTargets;
        }
    }
}