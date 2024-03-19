using System;
using System.Collections.Generic;
using Tzipory.Tools;
using UnityEngine;


public abstract class EffectTransitionLerp<T> where T : Enum
{
    public LerpValueConfig LerpValueConfig => _lerpValueConfig;

    public List<EffectTransitionLerpValue<T>> EffectValues => _effectValues;

    [SerializeField] private LerpValueConfig _lerpValueConfig;
    [SerializeField] private List<EffectTransitionLerpValue<T>> _effectValues;

    public virtual void StartTransitionEffect()
    {
        foreach (var effectValue in _effectValues)
        {
            LerpValuesHandler.Instance.SetValueByType(_lerpValueConfig, effectValue.ValueType, effectValue.StartValue, effectValue.EndValue, SetValue);
        }
    }

    public virtual void EndTransitionEffect()
    {
        foreach (var effectValue in _effectValues)
        {
            LerpValuesHandler.Instance.SetValueByType(_lerpValueConfig, effectValue.ValueType, effectValue.EndValue, effectValue.StartValue, SetValue);
        }
    }

    protected abstract void SetValue(T type, float value);
}

[Serializable]
public struct EffectTransitionLerpValue<T> where T : Enum
{
    public T ValueType;
    public float StartValue;
    public float EndValue;
}