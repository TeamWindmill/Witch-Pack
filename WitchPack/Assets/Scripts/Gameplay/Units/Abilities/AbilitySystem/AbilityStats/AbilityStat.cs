using System;
using Gameplay.Units.Stats;

namespace Gameplay.Units.Abilities.AbilitySystem.AbilityStats
{
    [Serializable]
    public class AbilityStat : BaseStat<AbilityStatType>
    {
        public AbilityStat(AbilityStatType statType, float baseValue) : base(statType, baseValue)
        {
        
        }
    }

    public enum AbilityStatType
    {
        Damage,
        Cooldown,
        Speed,
        TargetingRangeNotWorking, //not supported currently
        CastTime,
        Penetration,
        ExtraPenetrationPerKill,
        KillToIncreasePenetration,
        EnergyPointsOnCast,
        ProjectilesAmount,
        Duration,
        BounceAmount,
        DamageIncreasePerShot,
        Size,
        Heal,
        MovementSpeedSlow,
        Armor,
        DotDamage,
        HpRegen,
        TickInterval,
        FinalDamageModifier,
    }
}