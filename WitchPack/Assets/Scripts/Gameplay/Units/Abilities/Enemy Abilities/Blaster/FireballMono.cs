using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMono : ProjectileMono
{
    [SerializeField] private AoeFire _aoeFire;
    protected override void OnTargetHit(IDamagable target)
    {
        var fireball = _ability as FireballSO;
        target.Damageable.GetHit(_owner.DamageDealer, _ability);
        _aoeFire.gameObject.SetActive(true);
        _aoeFire.Init(_owner,_ability,fireball.Duration,fireball.AoeScale);
    }
}
