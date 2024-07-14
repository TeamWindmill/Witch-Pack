public class EnemyAbilityHandler : AbilityHandler
{
    private Enemy _enemyOwner;
    public EnemyAbilityHandler(BaseUnit owner) : base(owner)
    {
        _enemyOwner = owner as Enemy;
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