using Gameplay.Units.Abilities.AbilitySystem.AbilityStats;
using Gameplay.Units.Abilities.Shaman_Abilities.ToorAbilities.MultiShot.Configs;
using Managers;

namespace Gameplay.Units.Abilities.Shaman_Abilities.ToorAbilities.MultiShot
{
    public class Ricochet : MultiShot
    {
        public readonly RicochetSO Config;
        public Ricochet(RicochetSO config, BaseUnit owner) : base(config, owner)
        {
            Config = config;
            abilityStats.Add(new AbilityStat(AbilityStatType.BounceAmount,config.BounceAmount));
        }

        protected override MultiShotMono GetPooledObject()
        {
            return PoolManager.GetPooledObject<RicochetMono>();
        }
    }
}