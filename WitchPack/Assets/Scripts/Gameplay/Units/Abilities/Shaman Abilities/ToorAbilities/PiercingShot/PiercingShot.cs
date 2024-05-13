using UnityEngine;

public class PiercingShot : OffensiveAbility
{
    private PiercingShotSO _config;
    public PiercingShot(PiercingShotSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
    }

    public override bool CastAbility()
    {
        BaseUnit target = Owner.EnemyTargetHelper.GetTarget(CastingConfig.TargetData);
        if (!ReferenceEquals(target, null))
        {
            PiercingShotMono newPew = LevelManager.Instance.PoolManager.PiercingShotPool.GetPooledObject();
            newPew.transform.position = Owner.transform.position;
            newPew.gameObject.SetActive(true);
            Vector2 dir = (target.transform.position - Owner.transform.position) / (target.transform.position - Owner.transform.position).magnitude;
            newPew.Fire(Owner, _config, dir.normalized, _config.Penetration, true);
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