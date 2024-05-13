public class AutoAttackMono : ProjectileMono
{
    protected override void OnTargetHit(IDamagable target)
    {
        switch (target)
        {
            case CoreTemple:
                if (AbilitySo is EnemyRangedAutoAttackSO enemyAutoAttackSO)
                    target.Damageable.TakeFlatDamage(enemyAutoAttackSO.CoreDamage);
                break;
            case BaseUnit:
                target.Damageable.GetHit(_owner.DamageDealer, AbilitySo);
                break;
        }
    }
}