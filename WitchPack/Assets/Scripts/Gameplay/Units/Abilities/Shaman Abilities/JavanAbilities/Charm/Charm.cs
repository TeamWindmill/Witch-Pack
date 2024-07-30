using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities;
using Gameplay.Units.Damage_System;
using Gameplay.Units.Enemy.EnemyAIBehavior.GroundEnemies.States;

namespace Gameplay.Units.Abilities.Shaman_Abilities.JavanAbilities.Charm
{
    public class Charm : CastingAbility
    {
        private CharmSO _config;
        public Charm(CharmSO config, BaseUnit owner) : base(config, owner)
        {
            _config = config;
        }

        public override bool CastAbility(out IDamagable target)
        {
            var enemyTarget = Owner.EnemyTargetHelper.GetTarget(TargetData);

            if (ReferenceEquals(enemyTarget, null))
            {
                target = null;
                return false;
            }

            _config.CharmedState.StartCharm(enemyTarget);

            foreach (var statusEffect in StatusEffects)
            {
                if (!enemyTarget.Effectable.ContainsStatusEffect(statusEffect.StatusEffectVisual))
                {
                    var effect = enemyTarget.Effectable.AddEffect(statusEffect, Owner.Affector);
                    effect.Ended += _config.CharmedState.EndCharm;
                }
                else
                {
                    enemyTarget.Effectable.AddEffect(statusEffect, Owner.Affector);
                }
            }

            target = enemyTarget;
            return true;
        }

        public override bool CheckCastAvailable()
        {
            var target = Owner.EnemyTargetHelper.GetTarget(TargetData);
            return !ReferenceEquals(target, null) && target.EnemyAI.States.ContainsKey(typeof(Charmed));
        }
    }
}