using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Passive : BaseAbility
{
    public virtual void SubscribePassive(BaseUnit owner)
    {
    }

    public virtual void UnsubscribePassive(BaseUnit owner)
    {
    }
}