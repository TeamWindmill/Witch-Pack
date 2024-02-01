using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ability", menuName = "Ability/PenOnKillPiercingShot")]

public class PenOnKillPeircing : PiercingShot
{
    [SerializeField] private int extraPenPerKill;

    public override void OnSetCaster(BaseUnit caster)
    {
        AbilityEventCounter eventCounter = new AbilityEventCounter(caster, caster.AutoAttack, ref caster.DamageDealer.OnKill);
        eventCounter.OnCountIncrement += IncreasePen;
    }
    private void IncreasePen(AbilityEventCounter counter, Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbility ability)
    {
        dealer.Owner.Stats.AddValueToStat(StatType.AbilityProjectilePenetration, extraPenPerKill);
    }
}
