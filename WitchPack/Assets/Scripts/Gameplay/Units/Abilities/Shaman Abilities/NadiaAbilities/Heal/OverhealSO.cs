using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/Heal/Overheal")]
public class OverhealSO : HealSO
{
    [SerializeField] private int permanentMaxHealthBonus;

    protected override void HealTarget(Shaman target, BaseUnit caster)
    {
        if(target.Damageable.CurrentHp + healAmount > target.Stats.MaxHp)
        { 
            target.Stats.AddValueToStat(StatType.MaxHp, permanentMaxHealthBonus);
            target.ShamanVisualHandler.OverhealEffect.Play();
        }
        else
        {
            target.ShamanVisualHandler.HealEffect.Play();
        }
        target.Damageable.Heal(healAmount);
    }
}
