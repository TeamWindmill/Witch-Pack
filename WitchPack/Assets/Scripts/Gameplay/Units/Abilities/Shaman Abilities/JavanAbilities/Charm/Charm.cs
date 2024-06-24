public class Charm : CastingAbility
{
    private CharmSO _config;
    public Charm(CharmSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
    }

    public override bool CastAbility()
    {
        Enemy target = Owner.EnemyTargetHelper.GetTarget(_config.TargetData);

        if (ReferenceEquals(target, null)) return false;

        _config.CharmedState.StartCharm(target);

        foreach (var statusEffect in StatusEffects)
        {
            if (!target.Effectable.ContainsStatusEffect(statusEffect.StatusEffectType))
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
        var target = Owner.EnemyTargetHelper.GetTarget(_config.TargetData);
        return !ReferenceEquals(target, null) && target.EnemyAI.States.ContainsKey(typeof(Charmed));
    }
}