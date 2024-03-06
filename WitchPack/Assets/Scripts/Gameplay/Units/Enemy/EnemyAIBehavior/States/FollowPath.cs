using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/FollowPath", fileName = "FollowPath")]
public class FollowPath : IntervalState<EnemyAI>
{
    public override void Enter(EnemyAI parent)
    {
        parent.Enemy.Movement.ToggleMovement(false);
        base.Enter(parent);
    }

    protected override void IntervalUpdate(EnemyAI parent)
    {
        parent.Enemy.EnemyMovement.FollowPath();
    }

    protected override void IntervalChangeState(EnemyAI parent)
    {
        if (parent.Enemy.ShamanTargeter.HasTarget)
        {
            var rand = Random.Range(0f, 1f);
            if (rand < parent.AgroChance)
            {
                var target = parent.Enemy.ShamanTargetHelper.GetTarget(parent.TargetData);
                if(target.Stats.Visibility == 1) return;
                parent.SetTarget(target);
                parent.Enemy.Movement.Agent.stoppingDistance = parent.Enemy.Movement.DefaultStoppingDistance;
                parent.SetState(typeof(ChaseTarget));
            }
        }
    }

    public override void Exit(EnemyAI parent)
    {
        parent.Enemy.Movement.ToggleMovement(true);
        base.Exit(parent);
    }
}