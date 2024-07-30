using System;
using Sirenix.OdinInspector;

namespace Gameplay.Units.Stats
{
    [Serializable]
    public class Stats
    {
        //base stats here - affects every unit 
        public StatConfig MaxHp = new StatConfig(StatType.MaxHp);
        public StatConfig _damage = new StatConfig(StatType.BaseDamage);
        public StatDecimalConfig AttackSpeed = new StatDecimalConfig(StatType.AttackSpeed); //capped at unit stats 
        public StatDecimalConfig _range = new StatDecimalConfig(StatType.BaseRange);
        public StatDecimalConfig MovementSpeed = new StatDecimalConfig(StatType.MovementSpeed);
        public StatConfig CritDamage = new StatConfig(StatType.CritDamage);
        public StatConfig CritChance = new StatConfig(StatType.CritChance);
        public StatConfig Armor = new StatConfig(StatType.Armor);
        public StatConfig HpRegen = new StatConfig(StatType.HpRegen);
        public StatConfig HpRegenInterval = new StatConfig(StatType.HpRegenInterval);
        public StatConfig Threat = new StatConfig(StatType.Threat);
        [ReadOnly] public StatDecimalConfig AbilityCooldownReduction = new StatDecimalConfig(StatType.AbilityCooldownReduction, 1);
        [ReadOnly] public StatDecimalConfig EnergyGain = new StatDecimalConfig(StatType.EnergyGainMultiplier, 1);
        [ReadOnly] public StatConfig ThreatLevel = new StatConfig(StatType.ThreatLevel, 0);
        [ReadOnly] public StatConfig Invisibility = new StatConfig(StatType.Invisibility, 0);

        public float this[StatType statType]
        {
            get
            {
                switch (statType)
                {
                    case StatType.MaxHp:
                        return MaxHp.value;
                    case StatType.BaseDamage:
                        return _damage.value;
                    case StatType.AttackSpeed:
                        return AttackSpeed.value;
                    case StatType.BaseRange:
                        return _range.value;
                    case StatType.MovementSpeed:
                        return MovementSpeed.value;
                    case StatType.CritDamage:
                        return CritDamage.value;
                    case StatType.CritChance:
                        return CritChance.value;
                    case StatType.Armor:
                        return Armor.value;
                    case StatType.HpRegen:
                        return HpRegen.value;
                    case StatType.HpRegenInterval:
                        return HpRegenInterval.value;
                    case StatType.Threat:
                        return Threat.value;
                    case StatType.AbilityCooldownReduction:
                        return AbilityCooldownReduction.value;
                    case StatType.EnergyGainMultiplier:
                        return EnergyGain.value;
                    case StatType.ThreatLevel:
                        return ThreatLevel.value;
                    case StatType.Invisibility:
                        return Invisibility.value;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(statType), statType, null);
                }
            }
        }
    }

    [Serializable]
    public class StatConfig
    {
        [ReadOnly] public StatType statType;
        public int value;

        public StatConfig(StatType givenStatType)
        {
            statType = givenStatType;
        }

        public StatConfig(StatType statType, int value)
        {
            this.statType = statType;
            this.value = value;
        }
    }

    [Serializable]
    public class StatDecimalConfig
    {
        [ReadOnly] public StatType statType;
        public float value;

        public StatDecimalConfig(StatType givenStatType)
        {
            statType = givenStatType;
        }

        public StatDecimalConfig(StatType statType, float value)
        {
            this.statType = statType;
            this.value = value;
        }
    }
}