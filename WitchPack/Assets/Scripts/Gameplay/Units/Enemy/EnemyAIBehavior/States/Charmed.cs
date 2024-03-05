using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/Charmed", fileName = "Charmed")]
public class Charmed : State<EnemyAI>
{
    public override void Enter(EnemyAI parent)
    {
        base.Enter(parent);
        if(!parent.Enemy.Movement.IsActive)
            parent.Enemy.Movement.ToggleMovement(true);
    }
    public override void UpdateState(EnemyAI parent)
    {
        var target = parent.Enemy.EnemyTargeter.GetClosestTarget();
        if (!ReferenceEquals(target, null))
        {
            parent.Enemy.Movement.SetDestination(target.transform.position);
        }
    }

    public override void ChangeState(EnemyAI parent)
    {
        
    }
    public void EndCharm(Effectable parent,StatusEffect statusEffect)
    {
        (parent.Owner as Enemy)?.EnemyAI.SetState(typeof(ReturnToPath));
    }
    public override void Exit(EnemyAI parent)
    {
        base.Exit(parent);
        if(parent.Enemy.Movement.IsActive)
            parent.Enemy.Movement.ToggleMovement(false);
    }
}