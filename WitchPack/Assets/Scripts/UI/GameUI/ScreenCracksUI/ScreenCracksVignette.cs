using System;
using Tools.Lerp;
using UnityEngine;
using UnityEngine.UI;

public class ScreenCracksVignette : MonoEffectTransitionLerp<CracksVignetteValues>
{
    public float CurrentStartValue;
    [SerializeField] private Image _vignette;

    private void OnValidate()
    {
        _vignette ??= GetComponent<Image>();
    }

    private void Start()
    {
        foreach (var effectValue in EffectValues)
        {
            _vignette.material.SetFloat("_Scale",effectValue.StartValue);
        }

        CurrentStartValue = EffectValues[0].StartValue;
    }

    private void OnDisable()
    {
        foreach (var effectValue in EffectValues)
        {
            _vignette.material.SetFloat("_Scale",effectValue.StartValue);
        }
    }

    public override void StartTransitionEffect()
    {
        LerpValuesHandler.Instance.StartLerpByType(LerpValueConfig, EffectValues[0].ValueType, CurrentStartValue, EffectValues[0].EndValue, SetValue,OnTransitionEnd);
    }

    public override void EndTransitionEffect()
    {
        LerpValuesHandler.Instance.StartLerpByType(LerpValueConfig, EffectValues[0].ValueType, EffectValues[0].EndValue, CurrentStartValue, SetValue);
    }

    protected override void SetValue(CracksVignetteValues type, float value)
    {
        switch (type)
        {
            case CracksVignetteValues.Scale:
                _vignette.material.SetFloat("_Scale",value);
                break;
        }
    }

    protected override void OnTransitionEnd(float newValue)
    {
        EndTransitionEffect();
    }
}

public enum CracksVignetteValues
{
    Scale,
}