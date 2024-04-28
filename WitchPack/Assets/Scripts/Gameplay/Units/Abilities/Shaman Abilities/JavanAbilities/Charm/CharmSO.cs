using UnityEngine;

[CreateAssetMenu(fileName = "Charm", menuName = "Ability/Charm")]
public class CharmSO : CastingAbility
{
    [SerializeField] private Charmed _charmedState;
    public override bool CastAbility(BaseUnit caster)
    {
        Enemy target = caster.EnemyTargetHelper.GetTarget(TargetData);

        if (ReferenceEquals(target, null)) return false;
        
        _charmedState.StartCharm(target);
            
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

    public override bool CheckCastAvailable(BaseUnit caster)
    {
        var target = caster.EnemyTargetHelper.GetTarget(TargetData);
        return !ReferenceEquals(target, null) && target.EnemyAI.States.ContainsKey(typeof(Charmed));
    }
}