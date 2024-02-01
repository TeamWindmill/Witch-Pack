using UnityEngine;

public class SameTargetAutoAttackPassive : Passive
{
    [SerializeField] private EventToCount eventToCount;

    public override void SubscribePassive(BaseUnit owner)
    {
        //new counter 
        //cache abiltiy on counter (aa)
        //subscribe to on hit and check if the target is equal to the last target
        //subscription Happens Here
        AbilityEventCounter evetnCounter;
        switch (eventToCount)
        {
            case EventToCount.OnHit:
                evetnCounter = new AbilityEventCounter(owner, owner.AutoAttack, owner.DamageDealer.OnHitTarget);
                break;
            case EventToCount.OnKill:
                evetnCounter = new AbilityEventCounter(owner, owner.AutoAttack, owner.DamageDealer.OnKill);
                break;
        }

    }

    private void IncreaseAADamage()
    {
        
    }

}

public enum EventToCount
{
    OnHit,
    OnKill
}
