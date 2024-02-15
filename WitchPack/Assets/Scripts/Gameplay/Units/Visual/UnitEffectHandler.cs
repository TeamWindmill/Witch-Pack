using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEffectHandler : MonoBehaviour
{
    [SerializeField] private StatusEffectVisual[] _statusEffectVisuals;

    private Effectable _effectable;

    private void Start()
    {
        foreach (var effectVisual in _statusEffectVisuals)
        {
            effectVisual.Effect.gameObject.SetActive(false);
        }
    }

    public void PlayEffect(Effectable effectable,StatusEffectType statusEffectType)
    {
        _effectable = effectable;
        foreach (var effectVisual in _statusEffectVisuals)
        {
            if (effectVisual.StatusEffectType == statusEffectType)
            {
                effectable.OnEffectRemovedGFX += DisableEffect;
                effectVisual.Effect.gameObject.SetActive(true);
                return;
            }
        }
    }

    private void DisableEffect(StatusEffectType statusEffectType)
    {
        foreach (var effectVisual in _statusEffectVisuals)
        {
            if (effectVisual.StatusEffectType == statusEffectType)
            {
                _effectable.OnEffectRemovedGFX -= DisableEffect;
                effectVisual.Effect.gameObject.SetActive(false);
                return;
            }
        }
    }
}

[Serializable]
public struct StatusEffectVisual
{
    public StatusEffectType StatusEffectType;
    public ParticleSystem Effect;
}
