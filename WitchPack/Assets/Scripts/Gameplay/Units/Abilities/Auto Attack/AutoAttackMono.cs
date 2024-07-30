using Gameplay.Pools.Pool_System;
using Gameplay.Units.Abilities.AbilitiesMono.General_Abilities;
using Gameplay.Units.Damage_System;
using UnityEngine;

namespace Gameplay.Units.Abilities.Auto_Attack
{
    public class AutoAttackMono : ProjectileMono,IPoolable
    {
        protected override void OnTargetHit(IDamagable target)
        {
            switch (target)
            {
                case CoreTemple.CoreTemple:
                    if (Ability is EnemyRangedAutoAttack enemyAutoAttack)
                        target.Damageable.TakeFlatDamage(enemyAutoAttack.Config.CoreDamage);
                    break;
                case BaseUnit:
                    target.Damageable.GetHit(_owner.DamageDealer, Ability);
                    break;
            }
        }
        public GameObject PoolableGameObject => gameObject;
    }
}