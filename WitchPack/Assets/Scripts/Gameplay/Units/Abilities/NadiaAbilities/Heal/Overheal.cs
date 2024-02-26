using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/Heal/Overheal")]
public class Overheal : Heal
{
    [SerializeField] private StatusEffectConfig permanentMaxHealthBonus;

    protected override void HealTarget(Shaman target, BaseUnit caster)
    {
        if(target.Damageable.CurrentHp + healAmount > target.Stats.MaxHp)
        {
            target.Effectable.AddEffect(permanentMaxHealthBonus, caster.Affector);
        }
        target.Damageable.Heal(healAmount);
    }
}
