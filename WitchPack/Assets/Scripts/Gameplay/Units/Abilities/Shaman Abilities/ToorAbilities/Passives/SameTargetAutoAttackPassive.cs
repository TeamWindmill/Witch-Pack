
    public class SameTargetAutoAttackPassive : PassiveAbility
    {
        private SameTargetAutoAttackPassiveSO _config;
        public SameTargetAutoAttackPassive(SameTargetAutoAttackPassiveSO config, BaseUnit owner) : base(config, owner)
        {
            _config = config;
        }
        
        public override void SubscribePassive()
        {
            AttritionCounter eventCounter;
            switch (_config.EventToCount)
            {
                case EventToCount.OnHit:
                    eventCounter = new AttritionCounter(Owner, Owner.AutoAttack, ref Owner.DamageDealer.OnHitTarget, _config.MaxStacks);
                    eventCounter.OnCountIncrement += IncreaseAADamage;
                    break;
                case EventToCount.OnKill:
                    eventCounter = new AttritionCounter(Owner, Owner.AutoAttack, ref Owner.DamageDealer.OnKill, _config.MaxStacks);
                    eventCounter.OnCountIncrement += IncreaseAADamage;
                    break;
            }
        }

        private void IncreaseAADamage(AbilityEventCounter counter, Damageable target, DamageDealer dealer, DamageHandler dmg, AbilitySO abilitySo)
        {
            float mod = ((_config.DamageIncreasePerShot / 100) * counter.CurrentCount) + 1;
            dmg.AddMod(mod);
        }
    }
