using Gameplay.Units.Abilities;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAA", menuName = "Ability/MeleeAA")]
public class EnemyAutoAttack : OffensiveAbility
{
   
   public override bool CastAbility(BaseUnit caster)
   {
      BaseUnit target;
      if(caster.Effectable.ContainsStatusEffect(StatusEffectType.Charm))
      {
         target = caster.EnemyTargetHelper.GetTarget(TargetData,LevelManager.Instance.CharmedEnemies);
      }
      else
      {
         target = caster.ShamanTargetHelper.GetTarget(TargetData);
      }
      if (ReferenceEquals(target, null)) return false;
      if (Vector2.Distance(target.transform.position, caster.transform.position) > caster.Movement.DefaultStoppingDistance) return false;
      target.Damageable.GetHit(caster.DamageDealer,this);
      return true;
   }

   public override bool CheckCastAvailable(BaseUnit caster)
   {
      BaseUnit target;
      if(caster.Effectable.ContainsStatusEffect(StatusEffectType.Charm))
      {
         target = caster.EnemyTargetHelper.GetTarget(TargetData,LevelManager.Instance.CharmedEnemies);
      }
      else
      {
         target = caster.ShamanTargetHelper.GetTarget(TargetData);
      }
      return !ReferenceEquals(target, null);
   }
}
