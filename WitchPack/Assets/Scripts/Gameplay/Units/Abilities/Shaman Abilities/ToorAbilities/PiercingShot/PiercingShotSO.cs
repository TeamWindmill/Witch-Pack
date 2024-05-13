using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/PiercingShot")]

public class PiercingShotSO : OffensiveAbilitySO
{
    [SerializeField] private int penetration;
    public int Penetration { get => penetration; }

    public override bool CastAbility(BaseUnit caster)
    {
        BaseUnit target = caster.EnemyTargetHelper.GetTarget(TargetData);
        if (!ReferenceEquals(target, null))
        {
            PiercingShotMono newPew = LevelManager.Instance.PoolManager.PiercingShotPool.GetPooledObject();
            newPew.transform.position = caster.transform.position;
            newPew.gameObject.SetActive(true);
            Vector2 dir = (target.transform.position - caster.transform.position) / (target.transform.position - caster.transform.position).magnitude;
            newPew.Fire(caster, this, dir.normalized, penetration, true);
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
