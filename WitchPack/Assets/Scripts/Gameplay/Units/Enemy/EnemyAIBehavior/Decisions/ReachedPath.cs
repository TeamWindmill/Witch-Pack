using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/ReachedPath", fileName = "ReachedPath")]

public class ReachedPath : Decision
{
    
    public override bool Decide(BaseStateMachine machine)
    {
        var agent = machine.Owner.Movement.Agent;
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            return true;
        }
            
        return false;
    }
}