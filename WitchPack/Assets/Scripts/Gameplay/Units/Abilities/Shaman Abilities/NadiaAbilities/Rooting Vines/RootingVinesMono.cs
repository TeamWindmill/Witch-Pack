using UnityEngine;

public class RootingVinesMono : AoeMono
{
    
    protected virtual void OnRoot(Enemy enemy)
    {

    }

    protected override void OnEnemyEnter(Enemy enemy)
    {
        OnRoot(enemy);
        enemy.Damageable.GetHit(_owner.DamageDealer, _ability);
    }
}
