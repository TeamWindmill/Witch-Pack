using Tools.Targeter;

public class RockMonolith : OffensiveAbility
{
    private RockMonolithSO _config;
    private Shaman _shamanOwner;
    public RockMonolith(OffensiveAbilitySO config, BaseUnit owner) : base(config, owner)
    {
        _shamanOwner = Owner as Shaman;
        _config = config as RockMonolithSO;
        abilityStats.Add(new AbilityStat(AbilityStatType.Duration,_config.Duration));
        abilityStats.Add(new AbilityStat(AbilityStatType.Size,_config.TauntRadius));
    }

    public override bool CastAbility()
    {
        var targets = Owner.EnemyTargetHelper.GetAvailableTargets(_config.TargetData);
        
        //apply status effects on lila
        

        if (targets.Count > 0)
        {
            foreach (var target in targets)
            {
                target.ShamanTargetHelper.ApplyTaunt(_shamanOwner, GetAbilityStatValue(AbilityStatType.Duration));
                target.EnemyAI.SetState(typeof(Taunt));
            }

            return true;
        }
        
        return false;
    }

    public override bool CheckCastAvailable()
    {
        Enemy target = Owner.EnemyTargetHelper.GetTarget(_config.TargetData);
        if (!ReferenceEquals(target, null)) //might want to change to multiple enemies near her
        {
            return true;
        }

        return false;
    }
}