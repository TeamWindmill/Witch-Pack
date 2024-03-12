using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/ReturnToPath", fileName = "ReturnToPath")]
public class ReturnToPath : State<EnemyAI>
{
    public override void Enter(EnemyAI parent)
    {
        parent.Enemy.Movement.ToggleMovement(true);
        parent.Enemy.Movement.Agent.stoppingDistance = 0.1f;
        base.Enter(parent);
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
        parent.Enemy.Movement.Agent.stoppingDistance = parent.Enemy.Movement.DefaultStoppingDistance;
        base.Exit(parent);

    }
}