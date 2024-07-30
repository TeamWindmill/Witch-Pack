
using Gameplay.Units.Abilities.AbilitySystem.AbilityStats;
using Gameplay.Units.Abilities.Shaman_Abilities.NadiaAbilities.Heal.Configs;
using Gameplay.Units.Stats;
using UI.MapUI.MetaUpgrades.UpgradePanel.Configs;

namespace Gameplay.Units.Abilities.Shaman_Abilities.NadiaAbilities.Heal
{
    public class Overheal : Heal
    {
        private OverhealSO _config;
        public Overheal(OverhealSO config, BaseUnit owner) : base(config, owner)
        {
            _config = config;
            abilityStats.Add(new AbilityStat(AbilityStatType.Heal,config.HealAmount));
        }

        protected override void HealTarget(Shaman target, BaseUnit caster)
        {
            if (HasAbilityBehavior(AbilityBehavior.OverhealExcessHealing)) ExcessHealthGain(target);
            else FixedHealthGain(target);
        
            target.ShamanVisualHandler.OverhealEffect.Play();
            target.Damageable.Heal((int)GetAbilityStatValue(AbilityStatType.Heal));
        }

        private void ExcessHealthGain(Shaman target)
        {
            var healAmount = (int)GetAbilityStatValue(AbilityStatType.Heal);
            if(target.Damageable.CurrentHp + healAmount > target.Stats[StatType.MaxHp].Value)
            {
                var excessHeal = target.Damageable.CurrentHp + healAmount - target.Stats[StatType.MaxHp].Value;
                target.Stats.AddModifierToStat(StatType.MaxHp, excessHeal);
            }
        }

        private void FixedHealthGain(Shaman target)
        {
            var healAmount = (int)GetAbilityStatValue(AbilityStatType.Heal);
            if(target.Damageable.CurrentHp + healAmount > target.Stats[StatType.MaxHp].Value)
            { 
                target.Stats.AddModifierToStat(StatType.MaxHp, _config.PermanentMaxHealthBonus);
            }
        }
    }
}