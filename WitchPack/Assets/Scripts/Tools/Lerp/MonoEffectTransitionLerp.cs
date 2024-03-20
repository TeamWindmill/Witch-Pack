using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.Lerp
{
    public abstract class MonoEffectTransitionLerp<T> : MonoBehaviour where T : Enum
    {
        public LerpValueConfig LerpValueConfig => _lerpValueConfig;

        public List<EffectTransitionLerpValue<T>> EffectValues => _effectValues;

        [SerializeField] private LerpValueConfig _lerpValueConfig;
        [SerializeField] private List<EffectTransitionLerpValue<T>> _effectValues;

        public virtual void StartTransitionEffect()
        {
            foreach (var effectValue in _effectValues)
            {
                LerpValuesHandler.Instance.SetValueByType(_lerpValueConfig, effectValue.ValueType, effectValue.StartValue, effectValue.EndValue, SetValue,OnTransitionEnd);
            }
        }

        public virtual void EndTransitionEffect()
        {
            foreach (var effectValue in _effectValues)
            {
                LerpValuesHandler.Instance.SetValueByType(_lerpValueConfig, effectValue.ValueType, effectValue.EndValue, effectValue.StartValue, SetValue,OnTransitionEnd);
            }
        }

        protected abstract void SetValue(T type, float value);
        
        protected virtual void OnTransitionEnd(float newValue)
        {
        
        }
    }
}