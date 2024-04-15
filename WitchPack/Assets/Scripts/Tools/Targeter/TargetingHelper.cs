using System.Collections.Generic;
using System.Linq;
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
                target = GetTargetByStat(targets, givenData, targetsToAvoid);
                break;
            case TargetPriority.Distance:
                target = GetTargetByDistance(targets, givenData,originPos, targetsToAvoid);
                break;
            case TargetPriority.Random:
                target = targets[UnityEngine.Random.Range(0, targets.Count)];
                break;
            case TargetPriority.DistnaceToCore:
                target = GetTargetByDistanceToCore(targets, givenData, targetsToAvoid);
                break;
            case TargetPriority.Threatened:
                target = GetTargetByThreat(targets, givenData, targetsToAvoid);
                break;
            case TargetPriority.CurrentHP:
                target = GetTargetByHP(targets, givenData, targetsToAvoid);
                break;
            case TargetPriority.CurrentHPPrecentage:
                target = GetTargetByHPPrecentage(targets, givenData, targetsToAvoid);
                break;
            default:
                return targets[0];
        }
        return target;
    }

    public static T GetTargetByThreat(List<T> targets, TargetData targetData, List<T> targetsToAvoid)
    {
        //targets to avoid
        var availableTargets = targets;
        if (!ReferenceEquals(targetsToAvoid, null))
        {
            availableTargets = targets.Except(targetsToAvoid).ToList();
        }
        T cur = availableTargets.Count > 0 ? availableTargets[availableTargets.Count/2] : null;
        
        //checking for target from available targets
        for (int i = 0; i < availableTargets.Count; i++)
        {
            
            if(availableTargets[i].IsDead) continue;
            
            if(targetData.AvoidCharmedTargets)
                if(availableTargets[i].Effectable.ContainsStatusEffect(StatusEffectType.Charm)) continue;

            if (availableTargets[i].Stats.ThreatLevel <= 0) continue;
            
            if (targetData.Modifier == TargetModifier.Most)
            {
                if (cur.Stats.ThreatLevel < availableTargets[i].Stats.ThreatLevel)
                {
                    cur = availableTargets[i];
                }
            }
            else
            {
                if (cur.Stats.ThreatLevel > availableTargets[i].Stats.ThreatLevel)
                {
                    cur = availableTargets[i];
                } 
            }
        }
        return cur;
    }
    public static T GetTargetByHP(List<T> targets, TargetData targetData, List<T> targetsToAvoid = null)
    {
        //targets to avoid
        var availableTargets = targets;
        if (!ReferenceEquals(targetsToAvoid, null))
        {
            availableTargets = targets.Except(targetsToAvoid).ToList();
        }
        T cur = availableTargets.Count > 0 ? availableTargets[availableTargets.Count/2] : null;
        
        //checking for target from available targets
        for (int i = 0; i < availableTargets.Count; i++)
        {
            if(availableTargets[i].IsDead) continue;
            
            if(targetData.AvoidCharmedTargets)
                if(availableTargets[i].Effectable.ContainsStatusEffect(StatusEffectType.Charm)) continue;
            
            if (!ReferenceEquals(cur, null))
            {
                if (targetData.Modifier == TargetModifier.Most)
                {
                    if (cur.Damageable.CurrentHp < availableTargets[i].Damageable.CurrentHp)
                    {
                        cur = availableTargets[i];
                    }
                }
                else
                {
                    if (cur.Damageable.CurrentHp > availableTargets[i].Damageable.CurrentHp)
                    {
                        cur = availableTargets[i];
                    }
                }
            }
        }
        return cur;
    }
    public static T GetTargetByHPPrecentage(List<T> targets, TargetData targetData, List<T> targetsToAvoid = null)
    {
        //targets to avoid
        var availableTargets = targets;
        if (!ReferenceEquals(targetsToAvoid, null))
        {
            availableTargets = targets.Except(targetsToAvoid).ToList();
        }
        T cur = availableTargets.Count > 0 ? availableTargets[availableTargets.Count/2] : null;
        
        //checking for target from available targets
        for (int i = 0; i < availableTargets.Count; i++)
        {
            if(availableTargets[i].IsDead) continue;
            
            if(targetData.AvoidCharmedTargets)
                if(availableTargets[i].Effectable.ContainsStatusEffect(StatusEffectType.Charm)) continue;
            
            if (!ReferenceEquals(cur, null))
            {
                if (targetData.Modifier == TargetModifier.Most)
                {
                    if (cur.Damageable.CurrentHp / cur.Stats.GetStatValue(StatType.MaxHp) < availableTargets[i].Damageable.CurrentHp / availableTargets[i].Stats.GetStatValue(StatType.MaxHp))
                    {
                        cur = availableTargets[i];
                    }
                }
                else
                {
                    if (cur.Damageable.CurrentHp / cur.Stats.GetStatValue(StatType.MaxHp) > availableTargets[i].Damageable.CurrentHp / availableTargets[i].Stats.GetStatValue(StatType.MaxHp))
                    {
                        cur = availableTargets[i];
                    }
                }
            }
        }
        return cur;
    }
    public static T GetTargetByStat(List<T> targets, TargetData targetData, List<T> targetsToAvoid = null)
    {
        //targets to avoid
        var availableTargets = targets;
        if (!ReferenceEquals(targetsToAvoid, null))
        {
            availableTargets = targets.Except(targetsToAvoid).ToList();
        }
        T cur = availableTargets.Count > 0 ? availableTargets[availableTargets.Count/2] : null;
        
        //checking for target from available targets
        for (int i = 0; i < availableTargets.Count; i++)
        {
            if(availableTargets[i].IsDead) continue;
            
            if(targetData.AvoidCharmedTargets)
                if(availableTargets[i].Effectable.ContainsStatusEffect(StatusEffectType.Charm)) continue;
            
            if (!ReferenceEquals(cur, null))
            {
                if (targetData.Modifier == TargetModifier.Most)
                {
                    if (cur.Stats.GetStatValue(targetData.StatType) < availableTargets[i].Stats.GetStatValue(targetData.StatType))
                    {
                        cur = availableTargets[i];
                    }
                }
                else
                {
                    if (cur.Stats.GetStatValue(targetData.StatType) > availableTargets[i].Stats.GetStatValue(targetData.StatType))
                    {
                        cur = availableTargets[i];
                    }
                }
            }
        }
        return cur;
    }

    public static T GetTargetByDistance(List<T> targets, TargetData targetData,Transform originPos, List<T> targetsToAvoid = null)
    {
        //targets to avoid
        var availableTargets = targets;
        if (!ReferenceEquals(targetsToAvoid, null))
        {
            availableTargets = targets.Except(targetsToAvoid).ToList();
        }
        T cur = availableTargets.Count > 0 ? availableTargets[availableTargets.Count/2] : null;
        
        //checking for target from available targets
        for (int i = 0; i < availableTargets.Count; i++)
        {
            if(availableTargets[i].IsDead) continue;
            
            if(targetData.AvoidCharmedTargets)
                if(availableTargets[i].Effectable.ContainsStatusEffect(StatusEffectType.Charm)) continue;

            if (!ReferenceEquals(cur, null))
            {
                if (targetData.Modifier == TargetModifier.Most)
                {
                    if (Vector3.Distance(cur.transform.position, originPos.position) < Vector3.Distance(availableTargets[i].transform.position, originPos.position))
                    {
                        cur = availableTargets[i];
                    }
                }
                else
                {
                    if (Vector3.Distance(cur.transform.position, originPos.position) > Vector3.Distance(availableTargets[i].transform.position, originPos.position))
                    {
                        cur = availableTargets[i];
                    }
                }
            }
        }
        return cur;
    }

    public static T GetTargetByDistanceToCore(List<T> targets, TargetData targetData, List<T> targetsToAvoid = null)
    {
        //targets to avoid
        var availableTargets = targets;
        if (!ReferenceEquals(targetsToAvoid, null))
        {
            availableTargets = targets.Except(targetsToAvoid).ToList();
        }
        T cur = availableTargets.Count > 0 ? availableTargets[availableTargets.Count/2] : null;
        
        //checking for target from available targets
        for (int i = 0; i < availableTargets.Count; i++)
        {
            
            if(availableTargets[i].IsDead) continue;
            
            if(targetData.AvoidCharmedTargets)
                if(availableTargets[i].Effectable.ContainsStatusEffect(StatusEffectType.Charm)) continue;

            if (!ReferenceEquals(cur, null))
            {
                if (cur is Shaman shaman)
                {
                    if (targetData.Modifier == TargetModifier.Most)
                    {
                        if (Vector3.Distance(shaman.transform.position, LevelManager.Instance.CurrentLevel.CoreTemple.transform.position) < Vector3.Distance(availableTargets[i].transform.position, LevelManager.Instance.CurrentLevel.CoreTemple.transform.position))
                        {
                            cur = availableTargets[i];
                        }
                    }
                    else
                    {
                        if (Vector3.Distance(shaman.transform.position, LevelManager.Instance.CurrentLevel.CoreTemple.transform.position) > Vector3.Distance(availableTargets[i].transform.position, LevelManager.Instance.CurrentLevel.CoreTemple.transform.position))
                        {
                            cur = availableTargets[i];
                        }
                    }  
                }
                else if (cur is Enemy enemy)
                {
                    if (targetData.Modifier == TargetModifier.Most)
                    {
                        if (enemy.EnemyMovement.DistanceRemaining < (availableTargets[i] as Enemy)?.EnemyMovement.DistanceRemaining)
                        {
                            cur = availableTargets[i];
                        }
                    }
                    else
                    {
                        if (enemy.EnemyMovement.DistanceRemaining > (availableTargets[i] as Enemy)?.EnemyMovement.DistanceRemaining)
                        {
                            cur = availableTargets[i];
                        }
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