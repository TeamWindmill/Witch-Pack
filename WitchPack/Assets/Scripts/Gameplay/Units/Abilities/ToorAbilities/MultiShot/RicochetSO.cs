using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Ricochet", menuName = "Ability/Ricochet")]
public class RicochetSO : MultiShotSO
{
    [BoxGroup("Ricochet")][SerializeField] private TargetData _ricochetTargetData; 
    [BoxGroup("Ricochet")][SerializeField] private LayerMask _targetingLayer;
    [BoxGroup("Ricochet")][SerializeField] private float _bounceRange;
    [BoxGroup("Ricochet")][SerializeField] private float _bounceSpeed;
    [BoxGroup("Ricochet")][SerializeField] private float _bounceCurveSpeed;
    
    public TargetData RicochetTargetData => _ricochetTargetData;
    public float BounceRange => _bounceRange;
    public float BounceSpeed => _bounceSpeed;
    public float BounceCurveSpeed => _bounceCurveSpeed;
    public LayerMask TargetingLayer => _targetingLayer;

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
                var shotMono = LevelManager.Instance.PoolManager.RicochetPool.GetPooledObject();
                shotMono.transform.position = caster.CastPos.position;
                shotMono.gameObject.SetActive(true);
                if (i == 0)
                {
                    shotMono.Init(multiShotType,caster, _target1, this, dirAngle);
                }
                else if (i % 2 == 0)
                {
                    shotMono.Init(multiShotType,caster, _target2, this, dirAngle + offset);
                }
                else
                {
                    shotMono.Init(multiShotType,caster, _target3, this, dirAngle - offset);
                }
            }
            return true;
        }

        return false;
    }
}