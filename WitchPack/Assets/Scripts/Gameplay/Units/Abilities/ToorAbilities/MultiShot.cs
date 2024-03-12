using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MultiShot", menuName = "Ability/MultiShot")]
public class MultiShot : OffensiveAbility
{
    [SerializeField] private int numberOfShots;
    [SerializeField] private Vector3 offset;
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
                Vector3 dir = target.transform.position - caster.transform.position;
                if (i == 0)
                {
                    shotMono.Init(target,this, Quaternion.Euler(dir).eulerAngles - new Vector3(0,0,-90));
                }
                else if (i % 2 == 0)
                {
                    shotMono.Init(target,this,Quaternion.Euler(dir).eulerAngles + offset- new Vector3(0,0,-90));
                }
                else
                {
                    shotMono.Init(target,this,Quaternion.Euler(dir).eulerAngles - offset- new Vector3(0,0,-90));
                }
            }

            return true;
        }

        return false;
        // for (int i = 0; i < numberOfShots; i++)
        // {
        //     MultiShotMono shotMono = LevelManager.Instance.PoolManager.MultiShotPool.GetPooledObject();
        //     shotMono.transform.position = caster.CastPos.position;
        //     RicochetHandler richocheter = new RicochetHandler(shotMono, ricochetTimes, targetData, ricochetRange, ricochetSpeed);
        //     shotMono.gameObject.SetActive(true);
        //     Vector2 dir = foundTargets[i].transform.position - caster.transform.position;
        //     if (i == 0)
        //     {
        //         shotMono.Fire(caster, this, dir.normalized, foundTargets[i], Vector3.zero);
        //     }
        //     else if (i % 2 == 0)
        //     {
        //         shotMono.Fire(caster, this, dir.normalized, foundTargets[i], offset);
        //     }
        //     else
        //     {
        //         shotMono.Fire(caster, this, dir.normalized, foundTargets[i], -offset);
        //     }
        // }
        // return true;

    }

    public override bool CheckCastAvailable(BaseUnit caster)
    {
        var target = caster.EnemyTargetHelper.GetTarget(TargetData);
        if (target != null) return true;

        return false;
    }


}
