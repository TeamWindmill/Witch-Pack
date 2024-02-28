using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/Heal/Heal")]
public class Heal : OffensiveAbility
{
    [SerializeField] protected int healAmount;

    public override bool CastAbility(BaseUnit caster)
    {       
        Shaman target = caster.ShamanTargetHelper.GetTarget(TargetData); 
        if (!ReferenceEquals(target, null)) // any shaman in range?
        {
            // Check if caster has lower hp (ratio) than lowest hp target
            float maxHp = caster.Stats.MaxHp;
            float casterCurrentHpRatio = caster.Damageable.CurrentHp / maxHp;
            maxHp = target.Stats.MaxHp;
            float targetCurrentHpRatio = target.Damageable.CurrentHp / maxHp;
            if (casterCurrentHpRatio < targetCurrentHpRatio)
            {
                target = caster as Shaman;
            }

            if(target.Damageable.CurrentHp == target.Damageable.MaxHp)
            {
                return false;
            }

            HealTarget(target, caster);
            return true;
        }
        else // no other injured shamans
        {
            if(caster.Damageable.CurrentHp < caster.Stats.MaxHp) // check if caster is injured
            {
                HealTarget(caster as Shaman, caster);
                return true;
            }
            return false;
        }
    }

    public override bool CheckCastAvailable(BaseUnit caster)
    {
        Shaman target = caster.ShamanTargetHelper.GetTarget(TargetData); 
        if (!ReferenceEquals(target, null)) // any shaman in range?
        {
            // Check if caster has lower hp (ratio) than lowest hp target
            float casterCurrentHpRatio = caster.Damageable.CurrentHp / caster.Stats.MaxHp;
            float targetCurrentHpRatio = target.Damageable.CurrentHp / target.Stats.MaxHp;
            if (casterCurrentHpRatio < targetCurrentHpRatio)
            {
                target = caster as Shaman;
            }

            if(target.Damageable.CurrentHp == target.Damageable.MaxHp)
            {
                return false;
            }

            return true;
        }
        else // no other injured shamans
        {
            if(caster.Damageable.CurrentHp < caster.Stats.MaxHp) // check if caster is injured
            {
                return true;
            }
            return false;
        }
    }

    protected virtual void HealTarget(Shaman target, BaseUnit caster)
    {
        target.Damageable.Heal(healAmount);
        target.ShamanVisualHandler.HealEffect.Play();
    }
}
