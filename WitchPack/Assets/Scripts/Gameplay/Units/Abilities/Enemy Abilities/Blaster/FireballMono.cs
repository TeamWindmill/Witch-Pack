using UnityEngine;

public class FireballMono : ProjectileMono , IPoolable
{
    protected override void OnTargetHit(IDamagable target)
    {
        var fireball = Ability as Fireball;
        target.Damageable.GetHit(_owner.DamageDealer, Ability);
        var aoeFire = PoolManager.GetPooledObject<AoeFire>();
        aoeFire.Init(_owner,Ability,fireball.Config.Duration,fireball.Config.AoeScale);
        aoeFire.transform.position = target.GameObject.transform.position;
        aoeFire.gameObject.SetActive(true);
    }


    public GameObject PoolableGameObject => gameObject;
}
