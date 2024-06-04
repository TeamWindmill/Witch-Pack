public class FireballMono : ProjectileMono
{
    protected override void OnTargetHit(IDamagable target)
    {
        var fireball = Ability as Fireball;
        target.Damageable.GetHit(_owner.DamageDealer, Ability);
        var aoeFire = LevelManager.Instance.PoolManager.AoeFirePool.GetPooledObject();
        aoeFire.Init(_owner,Ability,fireball.Config.Duration,fireball.Config.AoeScale);
        aoeFire.transform.position = target.GameObject.transform.position;
        aoeFire.gameObject.SetActive(true);
    }
}
