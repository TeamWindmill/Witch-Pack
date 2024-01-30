using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MultiShot", menuName = "Ability/MultiShot")]
public class MultiShot : OffensiveAbility
{
    [SerializeField] private int numberOfShots;
    [SerializeField] private Vector3 offset;
    public override bool CastAbility(BaseUnit caster)
    {
        List<BaseUnit> foundTargets = new List<BaseUnit>();
        for (int i = 0; i < numberOfShots; i++)
        {
            BaseUnit target = caster.TargetHelper.GetTarget(caster.Targeter.AvailableTargets, TargetData, foundTargets);
            
            foundTargets.Add(target);

            if (ReferenceEquals(target, null))
            {
                return false;
            }   
        }
        for (int i = 0; i < numberOfShots; i++)
        {
            ArchedShot shot = LevelManager.Instance.PoolManager.ArchedShotPool.GetPooledObject();
            shot.transform.position = caster.CastPos.position;
            shot.gameObject.SetActive(true);
            Vector2 dir = foundTargets[i].transform.position - caster.transform.position;

            if (i == 0)
            {
                shot.Fire(caster, this, dir.normalized, foundTargets[i], Vector3.zero);
            }
            else if (i % 2 == 0)
            {
                shot.Fire(caster, this, dir.normalized, foundTargets[i], offset);
            }
            else
            {
                shot.Fire(caster, this, dir.normalized, foundTargets[i], -offset);
            }
        }
        return true;

    }
}
