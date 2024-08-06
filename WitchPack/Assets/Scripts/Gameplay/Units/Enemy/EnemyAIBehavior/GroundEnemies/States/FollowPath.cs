using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/FollowPath", fileName = "FollowPath")]
public class FollowPath : IntervalState<EnemyAI>
{
    [SerializeField,Range(0,1)] private float _agroChance;
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
            if (rand < _agroChance)
            {
                var target = parent.Enemy.ShamanTargetHelper.GetTarget(parent.Config.TargetData);
                if(target is null) return;
                if(target.Stats[StatType.Invisibility].IntValue > 0) return;
                parent.Enemy.Movement.Agent.stoppingDistance = parent.Enemy.Movement.DefaultStoppingDistance;
                parent.SetState(typeof(ChaseTarget));
            }
        }
    }
}