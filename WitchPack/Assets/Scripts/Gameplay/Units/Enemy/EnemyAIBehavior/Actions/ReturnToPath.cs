using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/ReturnToPath", fileName = "ReturnToPath")]
public class ReturnToPath : StateAction
{
    public override void Execute(BaseStateMachine machine)
    {
        machine.Owner.EnemyMovement.ReturnToPath();
    }
}