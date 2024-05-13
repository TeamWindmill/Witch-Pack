using System.Collections;
using Systems.ObjectPool;
using UnityEngine;
using UnityEngine.Events;


public class AutoAttackMono : ProjectileMono
{

    protected override void OnTargetHit(IDamagable target)
    {
        switch (target)
        {
            case CoreTemple:
                target.Damageable.TakeFlatDamage((_ability as AutoAttack).CoreDamage);
                break;
            case BaseUnit:
                target.Damageable.GetHit(_owner.DamageDealer, _ability);
                break;
        }
    }

}
