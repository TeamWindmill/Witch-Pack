using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effectable
{
    private BaseUnit owner;
    public Action<Effectable, Affector, StatusEffect> OnAffected;
    public Action<StatusEffect> OnEffectRemoved;
    private List<StatusEffect> activeEffects = new List<StatusEffect>();

    public BaseUnit Owner { get => owner;}

    public Effectable(BaseUnit owner)
    {
        this.owner = owner;
    }

    public void AddEffect(StatusEffectConfig givenEffectData, Affector affector)
    {
        StatusEffect ss = new StatusEffect(this, givenEffectData.Duration, givenEffectData.Amount, givenEffectData.StatTypeAffected, givenEffectData.Process, givenEffectData.StatusEffectType, givenEffectData.ValueType);
        for (int i = 0; i < activeEffects.Count; i++)//check if affected by a similar ss already
        {
            if (activeEffects[i].StatType == givenEffectData.StatTypeAffected && activeEffects[i].Process == givenEffectData.Process)
            {
                activeEffects[i] = ss;
                ss.Reset();
                ss.Activate();
                return;
            }
        }
        activeEffects.Add(ss);
        ss.Activate();
        OnAffected?.Invoke(this, affector, ss);
    }

    public void RemoveEffect(StatusEffect effect)
    {
        activeEffects.Remove(effect);
    }

   

}
