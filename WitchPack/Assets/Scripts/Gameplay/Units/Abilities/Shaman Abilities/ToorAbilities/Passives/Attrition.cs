
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
                    eventCounter = new AttritionCounter(Owner, Owner.AutoAttackCaster.Ability, ref Owner.DamageDealer.OnHitTarget, _config.MaxStacks);
                    eventCounter.OnCountIncrement += IncreaseAADamage;
                    break;
                case EventToCount.OnKill:
                    eventCounter = new AttritionCounter(Owner, Owner.AutoAttackCaster.Ability, ref Owner.DamageDealer.OnKill, _config.MaxStacks);
                    eventCounter.OnCountIncrement += IncreaseAADamage;
                    break;
            }
        }

        private void IncreaseAADamage(AbilityEventCounter counter, Damageable target, DamageDealer dealer, DamageHandler dmg, Ability ability)
        {
            float mod = ((GetAbilityStatValue(AbilityStatType.DamageIncreasePerShot) / 100) * counter.CurrentCount) + 1;
            dmg.AddMod(mod);
        }
    }
