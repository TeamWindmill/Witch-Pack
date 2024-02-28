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
                return true;
            }
        }

        return false;
    }
}