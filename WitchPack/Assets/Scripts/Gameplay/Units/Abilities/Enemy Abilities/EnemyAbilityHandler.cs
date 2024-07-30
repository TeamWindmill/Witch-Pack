using Gameplay.Units.Abilities.AbilitySystem;
using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities;
using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities.Passives;
using Gameplay.Units.Abilities.AbilitySystem.Casting;

namespace Gameplay.Units.Abilities.Enemy_Abilities
{
    public class EnemyAbilityHandler : AbilityHandler
    {
        private Enemy.Enemy _enemyOwner;
        public EnemyAbilityHandler(BaseUnit owner) : base(owner)
        {
            _enemyOwner = owner as Enemy.Enemy;
        }

        public void IntializeAbilities()
        {
            foreach (var abilitySo in _enemyOwner.EnemyConfig.Abilities)
            {
                var ability = AbilityFactory.CreateAbility(abilitySo, Owner);
                if (ability is PassiveAbility passive)
                {
                    passive.SubscribePassive();
                }
                else if (ability is CastingAbility castingAbility)
                {
                    var abilityCaster = new AbilityCaster(Owner, castingAbility);
                    CastingHandlers.Add(abilityCaster);
                    OnCasterAdded?.Invoke(abilityCaster);
                }
            }

            Owner.AutoCaster.Init(Owner, false);
        }
    }
}