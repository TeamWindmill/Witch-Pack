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

    public override bool CastAbility()
    {
        var target = Owner.EnemyTargetHelper.GetTarget(Config.TargetData);
        if (target != null)
        {
            var ability = LevelManager.Instance.PoolManager.OrbitalStonesPool.GetPooledObject();
            ability.transform.position = Owner.transform.position; //change orbit pos
            ability.Init(Owner,this,GetAbilityStatIntValue(AbilityStatType.ProjectilesAmount));
            ability.gameObject.SetActive(true);
            return true;
        }
        return false;
    }

    public override bool CheckCastAvailable()
    {
        var target = Owner.EnemyTargetHelper.GetTarget(Config.TargetData);
        if (target != null)
        {
            return true;
        }
        return false;
    }
}