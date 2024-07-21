using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/AttackCoreState", fileName = "AttackCoreState")]
public class AttackCoreState : State<EnemyAI>
{
    public override void Enter(EnemyAI parent)
    {
        parent.Enemy.AutoCaster.EnableCaster();
        base.Enter(parent);
    }

    public override void UpdateState(EnemyAI parent)
    {
        
    }

    public override void ChangeState(EnemyAI parent)
    {
        
    }
}