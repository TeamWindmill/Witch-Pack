
using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using Gameplay.Units.Abilities.Shaman_Abilities.LilaAbilities.RockMonolith.Configs;
using Managers;

namespace Gameplay.Units.Abilities.Shaman_Abilities.LilaAbilities.RockMonolith
{
    public class Aftershock : RockMonolith
    {
        public AftershockSO AftershockConfig;
        public Aftershock(OffensiveAbilitySO config, BaseUnit owner) : base(config, owner)
        {
            AftershockConfig = config as AftershockSO;
        }

        protected override void OnTauntEnd()
        {
        
            var aftershockMono = PoolManager.GetPooledObject<AftershockMono>();
            aftershockMono.transform.position = Owner.transform.position;
            aftershockMono.gameObject.SetActive(true);
            aftershockMono.Init(_shamanOwner,this,true,0);
        
            _shamanOwner.Damageable.OnHitGFX -= IncrementDamage;
            DamageIncrement = 0;
        }
    
    
    }
}