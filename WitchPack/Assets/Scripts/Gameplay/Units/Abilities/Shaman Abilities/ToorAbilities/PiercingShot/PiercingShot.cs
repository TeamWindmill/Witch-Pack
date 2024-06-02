using UnityEngine;

public class PiercingShot : OffensiveAbility
{
    private PiercingShotSO _config;
    public PiercingShot(PiercingShotSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
        abilityStats.Add(new AbilityStat(AbilityStatType.Penetration,_config.Penetration));
        abilityStats.Add(new AbilityStat(AbilityStatType.Speed,_config.Speed));
        abilityStats.Add(new AbilityStat(AbilityStatType.LifeTime,_config.LifeTime));
    }

    public override bool CastAbility()
    {
        BaseUnit target = Owner.EnemyTargetHelper.GetTarget(CastingConfig.TargetData);
        if (!ReferenceEquals(target, null))
        {
            PiercingShotMono newPew = LevelManager.Instance.PoolManager.PiercingShotPool.GetPooledObject();
            var position = Owner.transform.position;
            newPew.transform.position = position;
            newPew.gameObject.SetActive(true);
            var position1 = target.transform.position;
            Vector2 dir = (position1 - position) / (position1 - position).magnitude;
            newPew.Fire(Owner, this, dir.normalized, (int)GetAbilityStatValue(AbilityStatType.Penetration), true);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool CheckCastAvailable()
    {
        BaseUnit target = Owner.EnemyTargetHelper.GetTarget(CastingConfig.TargetData);
        return !ReferenceEquals(target, null);
    }
}