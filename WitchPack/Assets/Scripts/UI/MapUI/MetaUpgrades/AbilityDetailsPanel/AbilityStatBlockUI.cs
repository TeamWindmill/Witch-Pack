public class AbilityStatBlockUI : StatBlock<AbilityStatType>
{
    protected override string GetStatName(ref float baseValue, string statName, ref string modifier)
    {
        switch (statTypeId)
        {
            case AbilityStatType.Damage:
                statName = "Damage";
                break;
            case AbilityStatType.Cooldown:
                statName = "Cooldown";
                break;
            case AbilityStatType.Speed:
                statName = "Projectile Speed";
                break;
            case AbilityStatType.TargetingRangeNotWorking:
                break;
            case AbilityStatType.CastTime:
                statName = "CastTime";
                break;
            case AbilityStatType.Penetration:
                statName = "Penetration";
                break;
            case AbilityStatType.ExtraPenetrationPerKill:
                statName = "Extra Penetration Per Kill";
                break;
            case AbilityStatType.KillToIncreasePenetration:
                statName = "Kills To Increase Penetration";
                break;
            case AbilityStatType.EnergyPointsOnCast:
                statName = "Energy On Cast";
                break;
            case AbilityStatType.ProjectilesAmount:
                statName = "Projectiles Amount";
                break;
            case AbilityStatType.Duration:
                statName = "Duration";
                break;
            case AbilityStatType.BounceAmount:
                statName = "Bounce Amount";
                break;
            case AbilityStatType.DamageIncreasePerShot:
                statName = "Damage Increase Per Shot";
                break;
            case AbilityStatType.Size:
                statName = "Size";
                break;
            case AbilityStatType.Heal:
                statName = "Heal";
                break;
            case AbilityStatType.MovementSpeedSlow:
                statName = "Movement Speed Slow";
                break;
            case AbilityStatType.Armor:
                statName = "Armor";
                break;
            case AbilityStatType.DotDamage:
                statName = "Damage Over Time";
                break;
            case AbilityStatType.HpRegen:
                statName = "Regeneration";
                break;
            case AbilityStatType.TickInterval:
                statName = "Tick Interval";
                break;
            case AbilityStatType.FinalDamageModifier:
                statName = "Final Damage Modifier";
                break;
        }

        return statName;
    }
}