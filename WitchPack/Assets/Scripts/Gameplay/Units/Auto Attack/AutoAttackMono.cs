using System.Collections;
using Systems.ObjectPool;
using UnityEngine;
using UnityEngine.Events;


public class AutoAttackMono : ProjectileMono, IPoolable<AutoAttackMono>
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
    public int InitialStock { get; set; }
    public bool IsDynamic { get; set; }
    public AutoAttackMono FactoryMethod()
    {
        throw new System.NotImplementedException();
    }

    public void TurnOnCallback(AutoAttackMono obj)
    {
        throw new System.NotImplementedException();
    }

    public void TurnOffCallback(AutoAttackMono obj)
    {
        throw new System.NotImplementedException();
    }
}
