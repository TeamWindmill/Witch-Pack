using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/FollowPathBoss", fileName = "FollowPathBoss")]
public class FollowPathBoss : IntervalState<EnemyAI>
{
    [SerializeField] private float _distanceToCore;

    public override void Enter(EnemyAI parent)
    {
        parent.Enemy.Movement.ToggleMovement(false);
        base.Enter(parent);
    }

    protected override void IntervalUpdate(EnemyAI parent)
    {
        if (Vector3.Distance(parent.transform.position, LevelManager.Instance.CurrentLevel.CoreTemple.transform.position)
            < _distanceToCore)
            parent.SetState(typeof(AttackCoreState));

        parent.Enemy.EnemyMovement.FollowPath();
    }

    protected override void IntervalChangeState(EnemyAI parent)
    {
        if (parent.Enemy.ShamanTargeter.HasTarget)
        {
            var target = parent.Enemy.ShamanTargetHelper.GetTarget(parent.Config.TargetData);
            if (target.Stats.Visibility == 1) return;
            parent.SetState(typeof(AttackState));
        }
    }
}