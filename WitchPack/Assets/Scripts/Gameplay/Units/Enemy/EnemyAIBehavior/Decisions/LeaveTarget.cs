using Systems.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/LeaveTarget", fileName = "LeaveTarget")]
public class LeaveTarget : Decision
{
    [SerializeField] private float _returnChance;
    [SerializeField] private float _distanceModifier;
    public override bool Decide(BaseStateMachine machine)
    {
        var target = machine.Owner.ShamanTargeter.GetClosestTarget();
        if (ReferenceEquals(target, null)) return true;
        
        var rand = Random.Range(0f, 1f);
        var distance = _distanceModifier * Vector3.Distance(target.transform.position, machine.Owner.transform.position);
        if (rand < _returnChance * distance)
        {
            return true;
        }
        return false;
    }
}