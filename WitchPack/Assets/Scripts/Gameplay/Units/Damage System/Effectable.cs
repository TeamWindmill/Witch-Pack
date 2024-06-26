using System;
using System.Collections.Generic;

public class Effectable
{
    public event Action<Effectable, Affector, StatusEffect> OnAffected;
    public event Action<StatusEffectType> OnAffectedVFX;
    public event Action<StatusEffect> OnEffectRemoved;
    public event Action<StatusEffectType> OnEffectRemovedVFX;
    public IDamagable Owner => _owner;
    public List<StatusEffect> ActiveEffects => activeEffects;

    private List<StatusEffect> activeEffects = new List<StatusEffect>();
    private IDamagable _owner;

    public Effectable(IDamagable owner)
    { 
        _owner = owner;
    } 
    public StatusEffect AddEffect(StatusEffectData givenEffectData, Affector affector)
    {
        StatusEffect ss = new StatusEffect(this, givenEffectData);
        AddEffect(ss,affector);
        return ss;
    }
    public List<StatusEffect> AddEffects(List<StatusEffectData> givenEffectDatas, Affector affector)
    {
        List<StatusEffect> statusEffects = new();
        foreach (var data in givenEffectDatas)
        {
            StatusEffect ss = new StatusEffect(this, data);
            statusEffects.Add(AddEffect(ss,affector));
        }

        return statusEffects;
    }
    public StatusEffect AddEffect(StatusEffect givenEffect, Affector affector)
    {
        for (int i = 0; i < activeEffects.Count; i++)//check if affected by a similar ss already
        {
            if (activeEffects[i].StatType == givenEffect.StatType && activeEffects[i].Process == givenEffect.Process)
            {
                activeEffects[i] = givenEffect;
                givenEffect.Reset();
                givenEffect.Activate();
                return givenEffect;
            }
        }
        activeEffects.Add(givenEffect);
        givenEffect.Activate();
        OnAffected?.Invoke(this, affector, givenEffect);
        OnAffectedVFX?.Invoke(givenEffect.StatusEffectType); // maybe also added this to the if above? line 32
        return givenEffect;
    }

    public void RemoveEffect(StatusEffect effect)
    {
        activeEffects.Remove(effect);
        OnEffectRemoved?.Invoke(effect);
        OnEffectRemovedVFX?.Invoke(effect.StatusEffectType);
    }
    public void RemoveEffectsOfType(StatusEffectType effectType)
    {
        if(activeEffects.Count == 0) return;
        foreach (var effect in ActiveEffects)
        {
            if (effect.StatusEffectType == effectType)
            {
                RemoveEffect(effect);
            }
        }
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
