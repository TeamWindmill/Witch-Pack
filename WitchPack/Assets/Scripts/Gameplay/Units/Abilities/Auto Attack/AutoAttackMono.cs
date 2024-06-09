public class AutoAttackMono : ProjectileMono
{
    protected override void OnTargetHit(IDamagable target)
    {
        switch (target)
        {
            case CoreTemple:
                if (Ability is EnemyRangedAutoAttack enemyAutoAttack)
                    target.Damageable.TakeFlatDamage(enemyAutoAttack.Config.CoreDamage);
                break;
            case BaseUnit:
                target.Damageable.GetHit(_owner.DamageDealer, Ability);
                break;
        }
    }
}