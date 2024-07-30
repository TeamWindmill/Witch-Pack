
    using Gameplay.Units.Abilities.AbilitySystem.AbilityStats;
    using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities;
    using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities.Passives;
    using Gameplay.Units.Abilities.Shaman_Abilities.ToorAbilities.PiercingShot;
    using Gameplay.Units.Damage_System;

    namespace Gameplay.Units.Abilities.Shaman_Abilities.ToorAbilities.Passives
    {
        public class Attrition : PassiveAbility
        {
            private AttritionSO _config;
            public Attrition(AttritionSO config, BaseUnit owner) : base(config, owner)
            {
                _config = config;
                abilityStats.Add(new AbilityStat(AbilityStatType.DamageIncreasePerShot,config.DamageIncreasePerShot));
            }
        
            public override void SubscribePassive()
            {
                AttritionCounter eventCounter;
                switch (_config.EventToCount)
                {
                    case EventToCount.OnHit:
                        eventCounter = new AttritionCounter(Owner, Owner.AbilityHandler.AutoAttackCaster.Ability, ref Owner.DamageDealer.OnHitTarget, _config.MaxStacks);
                        eventCounter.OnCountIncrement += IncreaseAADamage;
                        break;
                    case EventToCount.OnKill:
                        eventCounter = new AttritionCounter(Owner, Owner.AbilityHandler.AutoAttackCaster.Ability, ref Owner.DamageDealer.OnKill, _config.MaxStacks);
                        eventCounter.OnCountIncrement += IncreaseAADamage;
                        break;
                }
            }

            private void IncreaseAADamage(AbilityEventCounter counter, Damageable target, DamageDealer dealer, DamageHandler dmg, Ability ability)
            {
                float mod = GetAbilityStatValue(AbilityStatType.DamageIncreasePerShot) * counter.CurrentCount;
                dmg.AddFlatMod((int)mod);
            }
        }
    }
