using Gameplay.Units.Abilities.AbilitySystem.AbilityStats;
using Gameplay.Units.Abilities.Shaman_Abilities.NadiaAbilities.Heal.Configs;

namespace Gameplay.Units.Abilities.Shaman_Abilities.NadiaAbilities.Heal
{
    public class BlessingOfSwiftness : Heal
    {
        private BlessingOfSwiftnessSO _config;
        public BlessingOfSwiftness(BlessingOfSwiftnessSO config, BaseUnit owner) : base(config, owner)
        {
            _config = config;
            abilityStats.Add(new AbilityStat(AbilityStatType.Heal,config.HealAmount));
        }

        protected override void HealTarget(Shaman target, BaseUnit caster)
        {
            target.Damageable.Heal((int)GetAbilityStatValue(AbilityStatType.Heal));
            target.ShamanVisualHandler.HealEffect.Play();
            target.Effectable.AddEffects(StatusEffects, caster.Affector);
        }
    }
}