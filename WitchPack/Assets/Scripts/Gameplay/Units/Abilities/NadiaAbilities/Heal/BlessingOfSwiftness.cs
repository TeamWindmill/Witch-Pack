using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/Heal/BlessingOfSwiftness")]
public class BlessingOfSwiftness : Heal
{
    [SerializeField] private StatusEffectConfig attackSpeedBoost;
    protected override void HealTarget(Shaman target, BaseUnit caster)
    {
        target.Damageable.Heal(healAmount);
        target.Effectable.AddEffect(attackSpeedBoost, caster.Affector);
    }
}
