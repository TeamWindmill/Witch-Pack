
    public class Reflect : PassiveAbility
    {
        private ReflectSO _config;
        public Reflect(AbilitySO baseConfig, BaseUnit owner) : base(baseConfig, owner)
        {
            _config = baseConfig as ReflectSO;
            abilityStats.Add(new AbilityStat(AbilityStatType.Damage,_config.ReflectedDamagePercent));
        }

        public override void SubscribePassive()
        {
            Owner.Damageable.OnTakeDamage += ReflectDamageBack;
        }

        private void ReflectDamageBack(Damageable damageable, DamageDealer damageDealer, DamageHandler damageHandler, Ability ability, bool isCrit)
        {
            var damageValue = GetAbilityStatValue(AbilityStatType.Damage) * damageHandler.GetDamage();
            var damage = new DamageHandler(damageValue);
            damageDealer.Owner.Damageable.TakeDamage(Owner.DamageDealer,damage,this,false);
        }
    }
