public class FireballMono : ProjectileMono
{
    protected override void OnTargetHit(IDamagable target)
    {
        var fireball = AbilitySo as FireballSO;
        target.Damageable.GetHit(_owner.DamageDealer, AbilitySo);
        var aoeFire = LevelManager.Instance.PoolManager.AoeFirePool.GetPooledObject();
        aoeFire.Init(_owner,AbilitySo,fireball.Duration,fireball.AoeScale);
        aoeFire.transform.position = target.GameObject.transform.position;
        aoeFire.gameObject.SetActive(true);
    }
}
