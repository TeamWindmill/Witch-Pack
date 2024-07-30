using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities;
using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using Gameplay.Units.Abilities.Auto_Attack.Configs;
using Gameplay.Units.Damage_System;
using Gameplay.Units.Stats;
using UnityEngine;

namespace Gameplay.Units.Abilities.Auto_Attack
{
   public class EnemyMeleeAutoAttack : OffensiveAbility
   {
      private EnemyMeleeAutoAttackSO _config;
      public EnemyMeleeAutoAttack(OffensiveAbilitySO config, BaseUnit owner) : base(config, owner)
      {
         _config = config as EnemyMeleeAutoAttackSO;
      }
   
      public override bool CastAbility(out IDamagable target)
      {
         if(Owner.Effectable.ContainsStatusEffect(StatusEffectVisual.Charm) || Owner.Effectable.ContainsStatusEffect(StatusEffectVisual.Frenzy))
         {
            target = Owner.EnemyTargetHelper.GetTarget(TargetData);
         }
         else
         {
            target = Owner.ShamanTargetHelper.GetTarget(TargetData);
         }
         if (ReferenceEquals(target, null)) return false;
         if (Vector2.Distance(target.GameObject.transform.position, Owner.transform.position) > Owner.Movement.DefaultStoppingDistance + _config.MeleeRange) return false;
         target.Damageable.GetHit(Owner.DamageDealer,this);
         return true;
      }

      public override bool CheckCastAvailable()
      {
         BaseUnit target;
         if(Owner.Effectable.ContainsStatusEffect(StatusEffectVisual.Charm) || Owner.Effectable.ContainsStatusEffect(StatusEffectVisual.Frenzy))
         {
            target = Owner.EnemyTargetHelper.GetTarget(TargetData);
         }
         else
         {
            target = Owner.ShamanTargetHelper.GetTarget(TargetData);
         }
         return !ReferenceEquals(target, null);
      }


   }
}
