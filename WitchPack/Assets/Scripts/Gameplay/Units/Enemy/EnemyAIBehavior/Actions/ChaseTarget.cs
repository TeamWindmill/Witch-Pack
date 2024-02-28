using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/ChaseTarget", fileName = "ChaseTarget")]
public class ChaseTarget : StateAction
{
    public override void Execute(BaseStateMachine machine)
    {
        var target = machine.Owner.ShamanTargeter.GetClosestTarget();
        machine.Owner.Movement.SetDest(target.transform.position);
    }
}