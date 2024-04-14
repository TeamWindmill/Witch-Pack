using UnityEngine;

[CreateAssetMenu(fileName = "Charm", menuName = "Ability/Charm")]
public class CharmSO : CastingAbility
{
    [SerializeField] private Charmed _charmedState;
    public override bool CastAbility(BaseUnit caster)
    {
        Enemy target = caster.EnemyTargetHelper.GetTarget(TargetData);
        
        if (!ReferenceEquals(target, null))
        {
            _charmedState.StartCharm(caster,target);
            LevelManager.Instance.CharmedEnemies.Add(target);
            
            foreach (var statusEffect in StatusEffects)
            {
                if (!target.Effectable.ContainsStatusEffect(statusEffect.StatusEffectType))
                {
                    var effect = target.Effectable.AddEffect(statusEffect, caster.Affector);
                    effect.Ended += _charmedState.EndCharm;
                }
                else
                {
                    target.Effectable.AddEffect(statusEffect, caster.Affector);
                }
            }

            return true;
        }

        return false;
    }

    public override bool CheckCastAvailable(BaseUnit caster)
    {
        BaseUnit target = caster.EnemyTargetHelper.GetTarget(TargetData);

        if (!ReferenceEquals(target, null))
        {
            return true;
        }

        return false;
    }
}