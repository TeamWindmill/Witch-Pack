using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAutoCaster : MonoBehaviour
{
    private BaseUnit owner;

    public bool CanCast;
    public void SetUp(BaseUnit givenOwner)
    {
        owner = givenOwner;
    }

    //turn comp on and off to attack;
    private void Update()
    {
        if (!CanCast)
        {
            return;
        }
        if (owner is Shaman shaman)
        {
            foreach (var castingHandler in shaman.CastingHandlers)
            {
                castingHandler.CastAbility();
            }
        }
        if (!ReferenceEquals(owner.AutoAttack, null))
        {
            owner.AutoAttackHandler.Attack();
        }
      
    }

}
