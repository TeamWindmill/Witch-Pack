using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SmokeBomb", menuName = "Ability/SmokeBomb")]
public class SmokeBombSO : OffensiveAbility
{
    public float Duration => _duration;

    [SerializeField] private float _duration;
    public override bool CastAbility(BaseUnit caster)
    {
        BaseUnit target = caster.ShamanTargetHelper.GetTarget(TargetData);
        if (!ReferenceEquals(target, null))
        {
            SmokeBomb smokeBomb = LevelManager.Instance.PoolManager.SmokeBombPool.GetPooledObject();
            smokeBomb.transform.position = target.transform.position;
            smokeBomb.gameObject.SetActive(true);
            smokeBomb.SpawnBomb(this,caster);
            return true;
        }
        else
        {
            return false;
        }
    }
}
