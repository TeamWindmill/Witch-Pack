using UnityEngine;


public class EnemyMeleeAutoAttack : OffensiveAbility
{
   
   public EnemyMeleeAutoAttack(OffensiveAbilitySO config, BaseUnit owner) : base(config, owner)
   {
   }
   
   public override bool CastAbility()
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
      if (ReferenceEquals(target, null)) return false;
      if (Vector2.Distance(target.transform.position, Owner.transform.position) > Owner.Movement.DefaultStoppingDistance) return false;
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
