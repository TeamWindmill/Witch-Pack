using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/Heal")]
public class Heal : BaseAbility
{
    [SerializeField] private int healAmount;

    public override bool CastAbility(BaseUnit caster)
    {       
        BaseUnit target = caster.ShamanTargetHelper.GetTarget(TargetData);
        if (!ReferenceEquals(target, null))
        {
            // Check if caster has lower hp (ratio) than lowest hp target
            float casterCurrentHpRatio = caster.Damageable.CurrentHp / caster.Stats.GetBaseStatValue(StatType.MaxHp);
            float targetCurrentHpRatio = target.Damageable.CurrentHp / target.Stats.GetBaseStatValue(StatType.MaxHp);
            if (casterCurrentHpRatio < targetCurrentHpRatio)
            {
                target = caster;
            }

            if(target.Damageable.CurrentHp == target.Damageable.MaxHp)
            {
                return false;
            }

            HealTarget(target);
            return true;
        }
        else // no other injured shamans
        {
            if(caster.Damageable.CurrentHp < caster.Stats.GetBaseStatValue(StatType.MaxHp)) // check if caster is injured
            {
                HealTarget(caster);
            }
            return false;
        }
    }

    protected virtual void HealTarget(BaseUnit target)
    {
        target.Damageable.Heal(healAmount);
    }
}
