using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectTransitionLerp
{
    public LerpValueConfig LerpValueConfig => _lerpValueConfig;

    public EffectTransitionLerpValue EffectValue => _effectValue;

    [SerializeField] private LerpValueConfig _lerpValueConfig;
    [SerializeField] private EffectTransitionLerpValue _effectValue;

    public virtual void StartTransitionEffect()
    {
        LerpValuesHandler.Instance.StartLerp(_lerpValueConfig, _effectValue.StartValue, _effectValue.EndValue, SetValue, OnTransitionEnd);
    }

    public virtual void EndTransitionEffect()
    {
        LerpValuesHandler.Instance.StartLerp(_lerpValueConfig, _effectValue.EndValue, _effectValue.StartValue, SetValue, OnTransitionEnd);
    }

    protected abstract void SetValue(float value);

    protected virtual void OnTransitionEnd(float newValue)
    {
    }
}

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
            LerpValuesHandler.Instance.StartLerpByType(_lerpValueConfig, effectValue.ValueType, effectValue.StartValue, effectValue.EndValue, SetValue, OnTransitionEnd);
        }
    }

    public virtual void EndTransitionEffect()
    {
        foreach (var effectValue in _effectValues)
        {
            LerpValuesHandler.Instance.StartLerpByType(_lerpValueConfig, effectValue.ValueType, effectValue.EndValue, effectValue.StartValue, SetValue, OnTransitionEnd);
        }
    }

    protected abstract void SetValue(T type, float value);

    protected virtual void OnTransitionEnd(float newValue)
    {
    }
}

[Serializable]
public struct EffectTransitionLerpValue
{
    public float StartValue;
    public float EndValue;
}

[Serializable]
public struct EffectTransitionLerpValue<T> where T : Enum
{
    public T ValueType;
    public float StartValue;
    public float EndValue;
}