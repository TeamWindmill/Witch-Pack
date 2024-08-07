using Systems.Pool_System;

public class OrbitalStones : OffensiveAbility
{
    public OrbitalStonesSO Config { get;}
    public OrbitalStones(OffensiveAbilitySO config, BaseUnit owner) : base(config, owner)
    {
        Config = config as OrbitalStonesSO;
        abilityStats.Add(new AbilityStat(AbilityStatType.Duration,Config.Duration));
        abilityStats.Add(new AbilityStat(AbilityStatType.ProjectilesAmount,Config.StoneAmount));
        abilityStats.Add(new AbilityStat(AbilityStatType.Size,Config.Radius));
        abilityStats.Add(new AbilityStat(AbilityStatType.Speed,Config.AngularSpeed));
        
    }

    public override bool CastAbility(out IDamagable target)
    {
        target = Owner.EnemyTargetHelper.GetTarget(TargetData);
        if (target != null)
        {
            Cast();
            return true;
        }
     
        return false;
    }
    public override bool ManualCast()
    {
        Cast();
        return true;
    }

    public override bool CheckCastAvailable()
    {
        var target = Owner.EnemyTargetHelper.GetTarget(TargetData);
        if (target != null)
        {
            return true;
        }
        return false;
    }

    private void Cast()
    {
        var ability = PoolManager.GetPooledObject<OrbitalStonesMono>();
        ability.transform.position = Owner.transform.position; //change orbit pos
        ability.Init(Owner,this);
    }
}