using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/RootingVines")]

public class RootingVines : OffensiveAbility
{
    [SerializeField] private float lastingTime;
    [SerializeField] private float _aoeScale = 1;
    public override bool CastAbility(BaseUnit caster)
    {
        BaseUnit target = caster.EnemyTargetHelper.GetTarget(TargetData);    
        if (!ReferenceEquals(target, null))
        {
            RootingVinesMono newVines = LevelManager.Instance.PoolManager.RootingVinesPool.GetPooledObject();
            newVines.Init(caster, this, lastingTime,_aoeScale);
            newVines.transform.position = target.transform.position;
            newVines.gameObject.SetActive(true);
            return true;
        }
        else
        {
            return false;
        }

    }

    public override bool CheckCastAvailable(BaseUnit caster)
    {
        BaseUnit target = caster.EnemyTargetHelper.GetTarget(TargetData);
        return !ReferenceEquals(target, null);
    }
}
