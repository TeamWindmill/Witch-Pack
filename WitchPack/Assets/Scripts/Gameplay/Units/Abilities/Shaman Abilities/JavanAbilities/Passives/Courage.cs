public class Courage : AffectedByUnitsStatPassive
{
    public Courage(AffectedByUnitsStatPassiveSO config, BaseUnit owner) : base(config, owner)
    {
    }

    public override void SubscribePassive()
    {
        base.SubscribePassive();
        bool foundAbility = false;
        foreach (var abilityCaster in Owner.AbilityHandler.CastingHandlers)
        {
            if (abilityCaster.Ability is Charm)
            {
                abilityCaster.OnCast += OnCharmCast;
                foundAbility = true;
            }
        }

        if (!foundAbility) Owner.AbilityHandler.OnCasterAdded += CheckIfCharmWasAdded;
    }

    private void CheckIfCharmWasAdded(AbilityCaster abilityCaster)
    {
        if (abilityCaster.Ability is Charm)
        {
            abilityCaster.OnCast += OnCharmCast;
        }
    }


    private void OnCharmCast(AbilityCaster abilityCaster, IDamagable target)
    {
        RefreshAffectingUnits();
        TimerManager.AddTimer(abilityCaster.Ability.StatusEffects[0].Duration.Value,RefreshAffectingUnits,true);

    }
}