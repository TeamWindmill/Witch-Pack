using System;
using System.Collections.Generic;

public class Effectable
{
    public event Action<Effectable, Affector, StatusEffect> OnAffected;
    public event Action<StatusEffectVisual> OnAffectedVFX;
    public event Action<StatusEffect> OnEffectRemoved;
    public event Action<StatusEffectVisual> OnEffectRemovedVFX;
    public IDamagable Owner => _owner;
    private Dictionary<StatusEffectVisual,StatusEffect> ActiveEffects { get; } = new ();

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
        for (int i = 0; i < ActiveEffects.Count; i++) //check if affected by a similar ss already
        {
            if (ActiveEffects.TryGetValue(givenEffect.StatusEffectVisual, out var effect))
            {
                effect.Reset();
                return effect;
            }
        }

        ActiveEffects.Add(givenEffect.StatusEffectVisual,givenEffect);
        givenEffect.Activate();
        OnAffected?.Invoke(this, affector, givenEffect);
        OnAffectedVFX?.Invoke(givenEffect.StatusEffectVisual);
        return givenEffect;
    }

    public void RemoveEffect(StatusEffect effect)
    {
        ActiveEffects.Remove(effect.StatusEffectVisual);
        OnEffectRemoved?.Invoke(effect);
        OnEffectRemovedVFX?.Invoke(effect.StatusEffectVisual);
    }

    public void RemoveEffectsOfType(StatusEffectVisual effectVisual)
    {
        if (ActiveEffects.Count == 0) return;
        if (ActiveEffects.TryGetValue(effectVisual, out var effect))
        {
            effect.Remove();
        }

        ActiveEffects.Remove(effectVisual);
    }

    public bool ContainsStatusEffect(StatusEffectVisual statusEffectVisual)
    {
        return ActiveEffects.ContainsKey(statusEffectVisual);
    }
}