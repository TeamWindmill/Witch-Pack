using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/ChaseTarget", fileName = "ChaseTarget")]
public class ChaseTarget : IntervalState<EnemyAI>
{
    

    public override void Enter(EnemyAI parent)
    {
        parent.CurrentTarget.Stats.AddValueToStat(StatType.ThreatLevel,1);
        base.Enter(parent);
    }

    protected override void IntervalUpdate(EnemyAI parent)
    {
        var target = parent.CurrentTarget;
        if(target is null) return;
        
        if (target.Stats.Visibility == 1 || target.IsDead)
        {
            parent.SetState(typeof(ReturnToPath));
        }
        parent.Enemy.Movement.SetDestination(target.transform.position);
    }

    protected override void IntervalChangeState(EnemyAI parent)
    {
        var target = parent.CurrentTarget;
        if (target is null)return;
        
        var rand = Random.Range(0f, 1f);
        var distance = parent.DistanceModifier * Vector3.Distance(target.transform.position, parent.Enemy.transform.position);
        if (rand < parent.ReturnChance * distance)
        {
            parent.Enemy.Movement.Agent.stoppingDistance = 0;
            parent.SetState(typeof(ReturnToPath));
        }
    }

    public override void Exit(EnemyAI parent)
    {
        if (parent.CurrentTarget != null)
        {
            parent.CurrentTarget.Stats.AddValueToStat(StatType.ThreatLevel,-1);
            parent.SetTarget(null);
        }
        base.Exit(parent);
    }
}