using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MultiShot", menuName = "Ability/MultiShot")]
public class MultiShot : OffensiveAbility
{
    [SerializeField] private int numberOfShots;
    [SerializeField] private float turnRate;
    [SerializeField] private Vector3 offset;
    [SerializeField, Range(0,90), Tooltip("Angle for the right shot, the left shot will be fired with the same angle only negative")] private float startAngle;

    public override bool CastAbility(BaseUnit caster)
    {
        for (int i = 0; i < numberOfShots; i++)
        {
            BaseUnit target = caster.TargetHelper.GetTarget(caster.Targeter.AvailableTargets, TargetData);
            if (ReferenceEquals(target, null))
            {
                return false;
            }
            ArchedShot shot = LevelManager.Instance.PoolManager.ArchedShotPool.GetPooledObject();
            shot.transform.position = caster.CastPos.position;
            shot.gameObject.SetActive(true);
            Vector2 dir = target.transform.position - caster.transform.position;
            switch (i)
            {
                case 0:
                    shot.Fire(caster, this, dir.normalized, target, offset);
                    break;
                case 1:
                    shot.Fire(caster, this, dir.normalized, target, offset);
                    break;
                case 2:
                    shot.Fire(caster, this, dir.normalized, target, -offset);
                    break;
                default:
                    break;
            }
        }
        return true;

    }
}
