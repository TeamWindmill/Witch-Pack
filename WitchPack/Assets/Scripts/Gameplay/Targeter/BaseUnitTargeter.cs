using UnityEngine;

public class BaseUnitTargeter<T> : Targeter<T> where T : BaseUnit
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        T possibleTarget = collision.GetComponent<T>();
        if (!ReferenceEquals(possibleTarget, null) && !availableTargets.Contains(possibleTarget))
        {
            if(possibleTarget.IsDead) return;
            availableTargets.Add(possibleTarget);
            OnTargetAdded?.Invoke(possibleTarget);
        }
    }
}