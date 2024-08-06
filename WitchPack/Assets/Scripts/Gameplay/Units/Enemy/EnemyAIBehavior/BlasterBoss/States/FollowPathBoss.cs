using System;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/FollowPathBoss", fileName = "FollowPathBoss")]
public class FollowPathBoss : FollowPath
{
    [SerializeField] private float _distanceToCore;
    
    protected override void IntervalUpdate(EnemyAI parent)
    {
        if (Vector3.Distance(parent.transform.position, LevelManager.Instance.CurrentLevel.CoreTemple.transform.position)
            < _distanceToCore)
            parent.SetState(typeof(AttackCoreState));

        base.IntervalUpdate(parent);
    }

    protected override void IntervalChangeState(EnemyAI parent)
    {
        if (parent.Enemy.ShamanTargeter.HasTarget)
        {
            var target = parent.Enemy.ShamanTargetHelper.GetTarget(parent.Config.TargetData);
            if (!ReferenceEquals(target,null) && target.Stats[StatType.Invisibility].IntValue > 0) return;
            parent.SetState(typeof(AttackState));
        }
    }

    public override Type GetStateType()
    {
        return typeof(FollowPath);
    }
}