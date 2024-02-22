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
    [SerializeField] private TargetData ricochetTargeting;//specific targeting for the ricochets 
    public override bool CastAbility(BaseUnit caster)
    {
        List<Enemy> foundTargets = new List<Enemy>();
        for (int i = 0; i < numberOfShots; i++)
        {
            Enemy target = caster.EnemyTargetHelper.GetTarget(TargetData, foundTargets);
            
            foundTargets.Add(target);

            if (ReferenceEquals(target, null))
            {
                return false;
            }   
        }
        for (int i = 0; i < numberOfShots; i++)
        {
            MultiShotMono shotMono = LevelManager.Instance.PoolManager.MultiShotPool.GetPooledObject();
            shotMono.transform.position = caster.CastPos.position;
            RicochetHandler richocheter = new RicochetHandler(shotMono, ricochetTimes, ricochetTargeting, ricochetRange, ricochetSpeed);
            shotMono.gameObject.SetActive(true);
            Vector2 dir = foundTargets[i].transform.position - caster.transform.position;
            if (i == 0)
            {
                shotMono.Fire(caster, this, dir.normalized, foundTargets[i], Vector3.zero);
            }
            else if (i % 2 == 0)
            {
                shotMono.Fire(caster, this, dir.normalized, foundTargets[i], offset);
            }
            else
            {
                shotMono.Fire(caster, this, dir.normalized, foundTargets[i], -offset);
            }
        }
        return true;

    }

    public override bool CheckCastAvailable(BaseUnit caster)
    {
        List<Enemy> foundTargets = new List<Enemy>();
        for (int i = 0; i < numberOfShots; i++)
        {
            Enemy target = caster.EnemyTargetHelper.GetTarget(TargetData, foundTargets);

            foundTargets.Add(target);

            if (ReferenceEquals(target, null))
            {
                return false;
            }
        }
        return true;
    }


}
