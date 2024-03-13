using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "MultiShot", menuName = "Ability/MultiShot")]
public class MultiShot : OffensiveAbility
{
    [SerializeField] private int offset;


    [SerializeField] private int ricochetTimes; //how many times the bullet will bounce between targets
    [SerializeField] private float ricochetRange;
    [SerializeField] private float ricochetSpeed;

    private const int NUMBER_OF_SHOTS = 3;
    private Enemy _target1;
    private Enemy _target2;
    private Enemy _target3;

    public override bool CastAbility(BaseUnit caster)
    {
        _target1 = null;
        _target2 = null;
        _target3 = null;
        
        //get an initial target
        _target1 = caster.EnemyTargetHelper.GetTarget(TargetData);
        if (!ReferenceEquals(_target1, null))
        {
            
            //target 2
            _target2 = caster.EnemyTargetHelper.GetTarget(TargetData, new []{_target1}.ToList()) ?? _target1;

            //target 3
            _target3 = caster.EnemyTargetHelper.GetTarget(TargetData, new []{_target1,_target2}.ToList()) ?? _target1;
            
            //calculate start direction
            var dir = _target1.transform.position - caster.transform.position;
            var dirAngle = Vector3.SignedAngle(Vector3.up, dir.normalized,Vector3.forward);

            for (int i = 0; i < NUMBER_OF_SHOTS; i++)
            {
                var shotMono = LevelManager.Instance.PoolManager.MultiShotPool.GetPooledObject();
                shotMono.transform.position = caster.CastPos.position;
                shotMono.gameObject.SetActive(true);
                if (i == 0)
                {
                    shotMono.Init(caster, _target1, this, dirAngle);
                }
                else if (i % 2 == 0)
                {
                    shotMono.Init(caster, _target2, this, dirAngle + offset);
                }
                else
                {
                    shotMono.Init(caster, _target3, this, dirAngle - offset);
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