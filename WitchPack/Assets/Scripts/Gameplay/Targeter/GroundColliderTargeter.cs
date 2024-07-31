using UnityEngine;

public class GroundColliderTargeter : Targeter<GroundCollider>
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        GroundCollider possibleTarget = collision.GetComponent<GroundCollider>();
        if (!ReferenceEquals(possibleTarget, null) && !availableTargets.Contains(possibleTarget))
        {
            if(possibleTarget.Unit.IsDead) return;
            availableTargets.Add(possibleTarget);
            OnTargetAdded?.Invoke(possibleTarget);
        }
    }
}
