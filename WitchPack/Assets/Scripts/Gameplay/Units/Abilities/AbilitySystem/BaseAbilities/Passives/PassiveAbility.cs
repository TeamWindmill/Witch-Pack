using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;

namespace Gameplay.Units.Abilities.AbilitySystem.BaseAbilities.Passives
{
    public abstract class PassiveAbility : Ability
    {
        public PassiveAbility(AbilitySO baseConfig, BaseUnit owner) : base(baseConfig, owner)
        {
        }

        public virtual void SubscribePassive()
        {
        }


        public virtual void UnsubscribePassive()
        {
        }
    }
}