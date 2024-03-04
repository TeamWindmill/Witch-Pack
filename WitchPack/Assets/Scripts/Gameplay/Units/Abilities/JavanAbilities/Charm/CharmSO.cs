using UnityEngine;

[CreateAssetMenu(fileName = "Charm", menuName = "Ability/Charm")]
public class CharmSO : OffensiveAbility
{
    [SerializeField] private Charmed _charmedState;
    public override bool CastAbility(BaseUnit caster)
    {
        BaseUnit target = caster.EnemyTargetHelper.GetTarget(TargetData);
        
        if (!ReferenceEquals(target, null))
        {
            foreach (var statusEffect in StatusEffects)
            {
                ((Enemy)target).EnemyAI.SetState(typeof(Charmed));
                if (!target.Effectable.ContainsStatusEffect(StatusEffectType.Charm))
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