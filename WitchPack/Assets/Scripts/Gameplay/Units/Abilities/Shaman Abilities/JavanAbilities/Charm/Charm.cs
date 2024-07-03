public class Charm : CastingAbility
{
    private CharmSO _config;
    public Charm(CharmSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
    }

    public override bool CastAbility()
    {
        Enemy target = Owner.EnemyTargetHelper.GetTarget(TargetData);

        if (ReferenceEquals(target, null)) return false;

        _config.CharmedState.StartCharm(target);

        foreach (var statusEffect in StatusEffects)
        {
            if (!target.Effectable.ContainsStatusEffect(statusEffect.StatusEffectVisual))
            {
                var effect = target.Effectable.AddEffect(statusEffect, Owner.Affector);
                effect.Ended += _config.CharmedState.EndCharm;
            }
            else
            {
                target.Effectable.AddEffect(statusEffect, Owner.Affector);
            }
        }

        return true;
    }

    public override bool CheckCastAvailable()
    {
        var target = Owner.EnemyTargetHelper.GetTarget(TargetData);
        return !ReferenceEquals(target, null) && target.EnemyAI.States.ContainsKey(typeof(Charmed));
    }
}