using System.Linq;
using UnityEngine;

public class Ricochet : MultiShot
{
    private RicochetSO _config;
    public Ricochet(RicochetSO config, BaseUnit owner) : base(config, owner)
    {
        _config = config;
    }

    public override bool CastAbility()
    {
        _target1 = null;
        _target2 = null;
        _target3 = null;
        
        //get an initial target
        _target1 = Owner.EnemyTargetHelper.GetTarget(_config.TargetData);
        if (!ReferenceEquals(_target1, null))
        {
            
            //target 2
            _target2 = Owner.EnemyTargetHelper.GetTarget(_config.TargetData, new []{_target1}.ToList());
            _target2 ??= _target1;
            
            //target 3
            _target3 = Owner.EnemyTargetHelper.GetTarget(_config.TargetData, new []{_target1,_target2}.ToList());
            _target3 ??= _target1;
            
            //calculate start direction
            var dir = _target1.transform.position - Owner.transform.position;
            var dirAngle = Vector3.SignedAngle(Vector3.up, dir.normalized,Vector3.forward);

            for (int i = 0; i < NUMBER_OF_SHOTS; i++)
            {
                var shotMono = LevelManager.Instance.PoolManager.RicochetPool.GetPooledObject();
                shotMono.transform.position = Owner.CastPos.position;
                shotMono.gameObject.SetActive(true);
                if (i == 0)
                {
                    shotMono.Init(_config.MultiShotType,Owner, _target1, _config, dirAngle);
                }
                else if (i % 2 == 0)
                {
                    shotMono.Init(_config.MultiShotType,Owner, _target2, _config, dirAngle + _config.Offset);
                }
                else
                {
                    shotMono.Init(_config.MultiShotType,Owner, _target3, _config, dirAngle - _config.Offset);
                }
            }
            return true;
        }

        return false;
    }
}