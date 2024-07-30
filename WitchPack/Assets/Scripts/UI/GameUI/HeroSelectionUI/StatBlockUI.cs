using Gameplay.Units.Stats;
using UI.Components;

namespace UI.GameUI.HeroSelectionUI
{
    public class StatBlockUI : StatBlock<StatType>
    {
        protected override string GetStatName(ref float baseValue, string statName, ref string modifier)
        {
            switch (statTypeId)
            {
                case StatType.MaxHp:
                    statName = "Health";
                    break;
                case StatType.BaseDamage:
                    statName = "Damage";
                    break;
                case StatType.AttackSpeed:
                    statName = "Attack Speed";
                    break;
                case StatType.BaseRange:
                    statName = "Range";
                    break;
                case StatType.MovementSpeed:
                    statName = "Move Speed";
                    break;
                case StatType.CritDamage:
                    statName = "Crit Damage";
                    baseValue += 100;
                    modifier = "%";
                    break;
                case StatType.CritChance:
                    statName = "Crit Chance";
                    modifier = "%";
                    break;
                case StatType.HpRegen:
                    statName = "Regeneration";
                    break;
                case StatType.Armor:
                    statName = "Armor";
                    break;
                case StatType.AbilityCooldownReduction:
                    statName = "Cooldown Reduction";
                    break;
            }

            return statName;
        }
    }
}
