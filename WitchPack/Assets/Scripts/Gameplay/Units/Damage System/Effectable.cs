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

    public StatusEffect AddEffect(StatusEffectConfig givenEffectData, Affector affector)
    {
        StatusEffect ss = new StatusEffect(this, givenEffectData);
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
        OnAffectedVFX?.Invoke(givenEffectData.StatusEffectType); // maybe also added this to the if above? line 32
        return ss;
    }

    public void RemoveEffect(StatusEffect effect)
    {
        activeEffects.Remove(effect);
        OnEffectRemoved?.Invoke(effect);
        OnEffectRemovedVFX?.Invoke(effect.StatusEffectType);
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
