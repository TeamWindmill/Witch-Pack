using UnityEngine;

public class PiercingShot : OffensiveAbility
{
    public int ProjectilePenetration { get; protected set; }

    public readonly PiercingShotSO PiercingShotConfig;
    public PiercingShot(PiercingShotSO config, BaseUnit owner) : base(config, owner)
    {
        PiercingShotConfig = config;
        abilityStats.Add(new AbilityStat(AbilityStatType.Penetration,PiercingShotConfig.Penetration));
        abilityStats.Add(new AbilityStat(AbilityStatType.Speed,PiercingShotConfig.Speed));
        abilityStats.Add(new AbilityStat(AbilityStatType.Duration,PiercingShotConfig.LifeTime));
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