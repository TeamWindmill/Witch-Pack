using PathCreation;
using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/FollowPath", fileName = "FollowPath")]
public class FollowPath : StateAction
{
    private float _dstTravelled;

    public override void Execute(BaseStateMachine stateMachine)
    {
        var owner = stateMachine.Owner;
        owner.EnemyMovement.FollowPath();
    }
}