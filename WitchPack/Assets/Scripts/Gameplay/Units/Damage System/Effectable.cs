using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effectable
{
    public event Action<Effectable, Affector, StatusEffect> OnAffected;
    public event Action<Effectable,StatusEffectType> OnAffectedGFX;
    public event Action<StatusEffect> OnEffectRemoved;
    public event Action<StatusEffectType> OnEffectRemovedGFX;
    public BaseUnit Owner => _owner;
    public List<StatusEffect> ActiveEffects => activeEffects;

    private List<StatusEffect> activeEffects = new List<StatusEffect>();
    private BaseUnit _owner;

    public Effectable(BaseUnit owner)
    { 
        _owner = owner;
    } 

    public StatusEffect AddEffect(StatusEffectConfig givenEffectData, Affector affector)
    {
        StatusEffect ss = new StatusEffect(this, givenEffectData.Duration, givenEffectData.Amount, givenEffectData.StatTypeAffected, givenEffectData.Process, givenEffectData.StatusEffectType, givenEffectData.ValueType);
        for (int i = 0; i < activeEffects.Count; i++)//check if affected by a similar ss already
        {
            if (activeEffects[i].StatType == givenEffectData.StatTypeAffected && activeEffects[i].Process == givenEffectData.Process)
            {
                activeEffects[i] = ss;
                ss.Reset();
                ss.Activate();
                return ss;
            }
        }
        activeEffects.Add(ss);
        ss.Activate();
        OnAffected?.Invoke(this, affector, ss);
        OnAffectedGFX?.Invoke(this,givenEffectData.StatusEffectType);
        return ss;
    }

    public void RemoveEffect(StatusEffect effect)
    {
        activeEffects.Remove(effect);
        OnEffectRemoved?.Invoke(effect);
        OnEffectRemovedGFX?.Invoke(effect.StatusEffectType);
    }

    public bool ContainsStatusEffect(StatusEffectType statusEffectType)
    {
        foreach (var statusEffect in ActiveEffects)
        {
            if (statusEffect.StatusEffectType == statusEffectType) return true;
        }

        return false;
    }
}
