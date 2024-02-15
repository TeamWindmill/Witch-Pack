using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/Heal")]
public class Heal : BaseAbility
{
    public override bool CastAbility(BaseUnit caster)
    {
        return base.CastAbility(caster);
    }
}
