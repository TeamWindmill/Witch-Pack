using UnityEngine;

[CreateAssetMenu(fileName = "SameTargetAA", menuName = "Ability/Passive/SameTargetAA")]
public class SameTargetAutoAttackPassive : Passive
{
    [SerializeField] private EventToCount eventToCount;
    [SerializeField] private int maxStacks;
    [SerializeField, Range(1, 100)] private float damageIncreasePerShot;
    public override void SubscribePassive(BaseUnit owner)
    {
        AttritionCounter evetnCounter;
        switch (eventToCount)
        {
            case EventToCount.OnHit:
                evetnCounter = new AttritionCounter(owner, owner.AutoAttack, ref owner.DamageDealer.OnHitTarget, maxStacks);
                evetnCounter.OnCountIncrement += IncreaseAADamage;
                break;
            case EventToCount.OnKill:
                evetnCounter = new AttritionCounter(owner, owner.AutoAttack, ref owner.DamageDealer.OnKill, maxStacks);
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
