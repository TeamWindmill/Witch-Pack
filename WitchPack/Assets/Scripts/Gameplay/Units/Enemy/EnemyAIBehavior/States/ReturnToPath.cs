using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/ReturnToPath", fileName = "ReturnToPath")]
public class ReturnToPath : State<EnemyAI>
{
    public override void Enter(EnemyAI parent)
    {
        base.Enter(parent);
        parent.Enemy.Movement.Agent.stoppingDistance = 0;
    }

    public override void UpdateState(EnemyAI parent)
    {
        parent.Enemy.EnemyMovement.ReturnToPath();
    }

    public override void ChangeState(EnemyAI parent)
    {
        var agent = parent.Enemy.Movement.Agent;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            parent.SetState(typeof(FollowPath));
        }
    }

    public override void Exit(EnemyAI parent)
    {
        base.Exit(parent);
        parent.Enemy.Movement.Agent.stoppingDistance = parent.Enemy.Movement.DefaultStoppingDistance;

    }
}