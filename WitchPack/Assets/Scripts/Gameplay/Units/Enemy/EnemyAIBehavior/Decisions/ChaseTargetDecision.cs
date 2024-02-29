using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/ChaseTarget", fileName = "ChaseTarget")]
public class ChaseTargetDecision : Decision
{
    [SerializeField] private float _chaseChance;
    public override bool Decide(BaseStateMachine machine)
    {
        if (machine.Owner.ShamanTargeter.HasTarget)
        {
            var rand = Random.Range(0f, 1f);
            if (rand < _chaseChance)
            {
                machine.Owner.EnemyAgro.CurrentTarget = machine.Owner.ShamanTargeter.GetClosestTarget();
                machine.Owner.Movement.Agent.stoppingDistance = machine.Owner.Movement.DefaultStoppingDistance;
                return true;
            }
        }

        return false;
    }
}