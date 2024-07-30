using Gameplay.Pools.Pool_System;
using Gameplay.Units.Abilities.AbilitiesMono.General_Abilities;
using Gameplay.Units.Damage_System;
using UnityEngine;

namespace Gameplay.Units.Abilities.Enemy_Abilities.Blaster
{
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
}
