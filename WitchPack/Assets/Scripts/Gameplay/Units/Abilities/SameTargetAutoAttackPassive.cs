using UnityEngine;

[CreateAssetMenu(fileName = "SameTargetAA", menuName = "Ability/Passive/SameTargetAA")]
public class SameTargetAutoAttackPassive : Passive
{
    [SerializeField] private EventToCount eventToCount;
    [SerializeField, Range(1, 100)] private float damageIncreasePerShot;
    public override void SubscribePassive(BaseUnit owner)
    {
        AbilityEventCounter evetnCounter;
        switch (eventToCount)
        {
            case EventToCount.OnHit:
                evetnCounter = new AbilityEventCounter(owner, owner.AutoAttack, ref owner.DamageDealer.OnHitTarget);
                evetnCounter.OnCountIncrement += IncreaseAADamage;
                break;
            case EventToCount.OnKill:
                evetnCounter = new AbilityEventCounter(owner, owner.AutoAttack, ref owner.DamageDealer.OnKill);
                evetnCounter.OnCountIncrement += IncreaseAADamage;
                break;
        }
    }

    private void IncreaseAADamage(AbilityEventCounter counter, Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbility ability)
    {
        float mod = ((damageIncreasePerShot / 100) * counter.CurrentCount) + 1;
        dmg.AddMod(mod);
    }

}

public enum EventToCount
{
    OnHit,
    OnKill
}
