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
            //cast
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
        return false;
    }


}
