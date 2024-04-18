using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMono : ProjectileMono
{
    protected override void OnTargetHit(IDamagable target)
    {
        var fireball = _ability as FireballSO;
        target.Damageable.GetHit(_owner.DamageDealer, _ability);
        var aoeFire = LevelManager.Instance.PoolManager.AoeFirePool.GetPooledObject();
        aoeFire.Init(_owner,_ability,fireball.Duration,fireball.AoeScale);
        aoeFire.transform.position = target.GameObject.transform.position;
        aoeFire.gameObject.SetActive(true);
    }
}
