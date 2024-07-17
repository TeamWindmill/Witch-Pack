using System;
using System.Collections.Generic;

public class Effectable
{
    public event Action<Effectable, Affector, StatusEffect> OnAffected;
    public event Action<StatusEffectVisual> OnAffectedVFX;
    public event Action<StatusEffect> OnEffectRemoved;
    public event Action<StatusEffectVisual> OnEffectRemovedVFX;
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
        AddEffect(ss, affector);
        return ss;
    }

    public List<StatusEffect> AddEffects(List<StatusEffectData> givenEffectDatas, Affector affector)
    {
        List<StatusEffect> statusEffects = new();
        foreach (var data in givenEffectDatas)
        {
            StatusEffect ss = new StatusEffect(this, data);
            statusEffects.Add(AddEffect(ss, affector));
        }

        return statusEffects;
    }

    public StatusEffect AddEffect(StatusEffect givenEffect, Affector affector)
    {
        for (int i = 0; i < activeEffects.Count; i++) //check if affected by a similar ss already
        {
            if (activeEffects[i].StatusEffectVisual == givenEffect.StatusEffectVisual && activeEffects[i].Process == givenEffect.Process)
            {
                activeEffects[i].Reset();
                return activeEffects[i];
            }
        }

        activeEffects.Add(givenEffect);
        givenEffect.Activate();
        OnAffected?.Invoke(this, affector, givenEffect);
        OnAffectedVFX?.Invoke(givenEffect.StatusEffectVisual);
        return givenEffect;
    }

    public void RemoveEffect(StatusEffect effect)
    {
        activeEffects.Remove(effect);
        OnEffectRemoved?.Invoke(effect);
        OnEffectRemovedVFX?.Invoke(effect.StatusEffectVisual);
    }

    public void RemoveEffectsOfType(StatusEffectVisual effectVisual)
    {
        if (activeEffects.Count == 0) return;
        foreach (var effect in ActiveEffects)
        {
            if (effect.StatusEffectVisual == effectVisual)
            {
                RemoveEffect(effect);
            }
        }
    }

    public bool ContainsStatusEffect(StatusEffectVisual statusEffectVisual)
    {
        foreach (var statusEffect in ActiveEffects)
        {
            if (statusEffect.StatusEffectVisual == statusEffectVisual) return true;
        }

        return false;
    }
}