using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMono : ProjectileMono
{
    protected override void OnTargetHit(IDamagable target)
    {
        target.Damageable.GetHit(owner.DamageDealer, ability);
    }
}
