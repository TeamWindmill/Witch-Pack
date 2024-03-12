using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MultiShot", menuName = "Ability/MultiShot")]
public class MultiShot : OffensiveAbility
{
    [SerializeField] private int numberOfShots;
    [SerializeField] private int offset;
    [SerializeField] private int ricochetTimes; //how many times the bullet will bounce between targets
    [SerializeField] private float ricochetRange;
    [SerializeField] private float ricochetSpeed;
    public override bool CastAbility(BaseUnit caster)
    {
        var target = caster.EnemyTargetHelper.GetTarget(TargetData);
        if (target != null)
        {
            for (int i = 0; i < numberOfShots; i++)
            {
                MultiShotMono shotMono = LevelManager.Instance.PoolManager.MultiShotPool.GetPooledObject();
                shotMono.transform.position = caster.CastPos.position;
                shotMono.gameObject.SetActive(true);
                var dir = target.transform.position - caster.transform.position;
                var dirAngle = Vector3.Angle(Vector3.up, dir.normalized);
                if (i == 0)
                {
                    shotMono.Init(caster,target,this, dirAngle);
                }
                else if (i % 2 == 0)
                {
                    shotMono.Init(caster,target,this,dirAngle + offset);
                }
                else
                {
                    shotMono.Init(caster,target,this,dirAngle - offset);
                }
            }

            return true;
        }

        return false;
    }

    public override bool CheckCastAvailable(BaseUnit caster)
    {
        var target = caster.EnemyTargetHelper.GetTarget(TargetData);
        if (target != null) return true;

        return false;
    }
}
