using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAutoAttacker : MonoBehaviour
{
    private BaseUnit owner;

    public bool CanAttack;
    public void SetUp(BaseUnit givenOwner)
    {
        owner = givenOwner;
    }

    //turn comp on and off to attack;
    private void Update()
    {
        if (!CanAttack)
        {
            return;
        }
        if (owner is Shaman shaman)
        {
            foreach (var item in shaman.CastingHandlers)
            {
                item.CastAbility();
            }
            if (!ReferenceEquals(shaman.AutoAttack, null))
            {
                shaman.AutoAttackHandler.Attack();
            }
        }
      
    }

}
