using System;

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
    EnergyPointsOnKill,
    ProjectilesAmount,
    Duration,
    BounceAmount,
    DamageIncreasePerShot,
    Size,
    Heal,
    MovementSpeedSlow,
    Armor,
    DotDamage,
}
