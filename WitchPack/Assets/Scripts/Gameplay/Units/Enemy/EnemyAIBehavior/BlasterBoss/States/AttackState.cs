using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/AttackState", fileName = "AttackState")]
public class AttackState : State<EnemyAI>
{
    [SerializeField] private float _attackStateDuration;

    public override void Enter(EnemyAI parent)
    {
        parent.Enemy.AutoCaster.EnableCaster();
        
        TimerManager.AddTimer(_attackStateDuration, parent ,EndState, true);
        base.Enter(parent);
    }

    public override void UpdateState(EnemyAI parent)
    {
        
    }

    public override void ChangeState(EnemyAI parent)
    {
        var target = parent.Enemy.ShamanTargetHelper.CurrentTarget;
        if (target is null)
        {
            parent.SetState(typeof(FollowPath));
            return;
        }

        if (target.Stats[StatType.Invisibility].IntValue > 0 || target.IsDead)
        {
            parent.SetState(typeof(FollowPath));
        }
    }
    public void EndState(EnemyAI parent)
    {
        if(parent is null) return;
        parent.SetState(typeof(FollowPath));
    }

    public override void Exit(EnemyAI parent)
    {
        parent.Enemy.AutoCaster.DisableCaster();
        parent.Enemy.ShamanTargetHelper.RemoveCurrentTarget();
        base.Exit(parent);
    }
}