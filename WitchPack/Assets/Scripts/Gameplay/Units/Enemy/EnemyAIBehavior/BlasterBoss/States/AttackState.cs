using System.Collections;
using System.Collections.Generic;
using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/AttackState", fileName = "AttackState")]
public class AttackState : State<EnemyAI>
{
    [SerializeField] private float _attackStateDuration;
    private float _timer;

    public override void Enter(EnemyAI parent)
    {
        parent.Enemy.AutoCaster.EnableCaster();
        _timer = _attackStateDuration;
        base.Enter(parent);
    }

    public override void UpdateState(EnemyAI parent)
    {
        _timer -= GAME_TIME.GameDeltaTime;

        if (_timer <= 0) parent.SetState(typeof(FollowPathBoss));
    }

    public override void ChangeState(EnemyAI parent)
    {
        var target = parent.Enemy.ShamanTargetHelper.CurrentTarget;
        if (target is null)
        {
            parent.SetState(typeof(FollowPathBoss));
            return;
        }

        if (target.Stats.Visibility == 1 || target.IsDead)
        {
            parent.SetState(typeof(FollowPathBoss));
        }
    }

    public override void Exit(EnemyAI parent)
    {
        parent.Enemy.AutoCaster.DisableCaster();
        parent.Enemy.ShamanTargetHelper.RemoveCurrentTarget();
        base.Exit(parent);
    }
}