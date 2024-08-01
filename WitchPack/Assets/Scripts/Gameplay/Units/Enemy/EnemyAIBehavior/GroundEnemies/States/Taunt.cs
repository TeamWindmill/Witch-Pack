using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/Taunt", fileName = "Taunt")]
public class Taunt : IntervalState<EnemyAI>
{
    public override void Enter(EnemyAI parent)
    {
        parent.Enemy.Movement.ToggleMovement(true);
        parent.Enemy.AutoCaster.EnableCaster();
        parent.Enemy.Movement.SetDestination(parent.Enemy.ShamanTargetHelper.CurrentTarget.transform.position);
        parent.Enemy.EnemyVisualHandler.EnemyEffectHandler.PlayEffect(StatusEffectVisual.Taunt);
        base.Enter(parent);
    }

    protected override void IntervalUpdate(EnemyAI parent)
    {
        var target = parent.Enemy.ShamanTargetHelper.CurrentTarget;
        if (target is null) return;
        
        parent.Enemy.Movement.SetDestination(target.transform.position);
    }

    protected override void IntervalChangeState(EnemyAI parent)
    {
        var target = parent.Enemy.ShamanTargetHelper.CurrentTarget;
        if (target is null)
        {
            parent.SetState(typeof(ReturnToPath));
            return;
        }
        
        if (/*target.Stats[StatType.Visibility].IntValue == 1 ||*/ target.IsDead)
        {
            parent.SetState(typeof(ReturnToPath));
        }
    }

    public void EndTaunt(Effectable parent,StatusEffect statusEffect)
    {
        (parent.Owner as Enemy)?.EnemyAI.SetState(typeof(ReturnToPath));
    }
    public override void Exit(EnemyAI parent)
    {
        parent.Enemy.AutoCaster.DisableCaster();
        parent.Enemy.EnemyVisualHandler.EnemyEffectHandler.DisableEffect(StatusEffectVisual.Taunt);
        base.Exit(parent);
    }
}