using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/ReachedPath", fileName = "ReachedPath")]

public class ReachedPath : Decision
{
    
    public override bool Decide(BaseStateMachine machine)
    {
        var movementAgent = machine.Owner.Movement;
        if(movementAgent.CurrentDestination == machine.Owner.transform.position)
        {
            return true;
        }
            
        return false;
    }
}