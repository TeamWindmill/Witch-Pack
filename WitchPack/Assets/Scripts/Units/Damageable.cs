using System;
using UnityEngine;
[System.Serializable]
public class Damageable
{
    private BaseUnit owner;
    private int currentHp;
    private int maxHp;

    public Action<Damageable, DamageDealer /*as of this moment might be null*/, DamageHandler, BaseAbility, bool /*critical - a more generic callback*/> OnGetHit;
    public Action<Damageable, DamageDealer /*as of this moment might be null*/, DamageHandler, BaseAbility> OnDeath;
    
    //add gfx events later

    public BaseUnit Owner { get => owner; }

    public Damageable(BaseUnit owner)
    {
        this.owner = owner;
    }

    public void GetHit(DamageDealer dealer, BaseAbility ability)
    {
        //calc hit? -> return
        //status effects addition
        foreach (var item in ability.StatusEffects)
        {
            owner.Effectable.AddEffect(item, dealer.Owner.Affector);
        }
        DamageHandler dmg = new DamageHandler(ability.BaseDamage);
        //calc crit
        if (UnityEngine.Random.Range(0,100) <= dealer.Owner.Stats.CritChance)
        {
            dealer.OnHitTarget?.Invoke(this, dealer, dmg, ability, true);
            OnGetHit?.Invoke(this, dealer, dmg, ability, true);
            dmg.AddMod((dealer.Owner.Stats.CritDamage / 100) + 1);//not sure what the math is supposed to be here
        }
        else
        {
            dealer.OnHitTarget?.Invoke(this, dealer, dmg, ability, false);
            OnGetHit?.Invoke(this, dealer, dmg, ability, false);

        }
        TakeDamage(dmg, dealer, ability);
        //trigger hit event
    }

    public void TakeDamage(DamageHandler handler, DamageDealer dealer, BaseAbility attack)
    {
        currentHp -= handler.GetFinalDamage();
        if (currentHp <= 0)
        {
            OnDeath?.Invoke(this, dealer, handler, attack);
            dealer.OnKill?.Invoke(this, dealer, handler, attack);
        }
        ClampHp();
    }

    public void TakeFlatDamage(int amount)  //DOES NOT TRIGGER EVENTS!
    {
        currentHp -= amount;
        if (currentHp <= 0)
        {
          //gfx here
        }
        ClampHp();
    }



    private void ClampHp()
    {
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
    }



}
