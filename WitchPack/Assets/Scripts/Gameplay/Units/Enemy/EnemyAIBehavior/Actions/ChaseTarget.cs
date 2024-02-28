using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/ChaseTarget", fileName = "ChaseTarget")]
public class ChaseTarget : StateAction
{
    public override void Execute(BaseStateMachine machine)
    {
        var target = machine.Owner.EnemyAgro.CurrentTarget;
        machine.Owner.Movement.SetDestination(target.transform.position);
    }
}