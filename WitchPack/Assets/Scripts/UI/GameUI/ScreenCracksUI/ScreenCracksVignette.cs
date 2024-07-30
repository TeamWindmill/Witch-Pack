using Tools.Lerp;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameUI.ScreenCracksUI
{
    public class ScreenCracksVignette : MonoEffectTransitionLerp<CracksVignetteValues>
    {
        public float CurrentStartValue;
        public float CurrentEndValue;
        [SerializeField] private Image _vignette;

        private void OnValidate()
        {
            _vignette ??= GetComponent<Image>();
        }

        public void SetStartValue()
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
            LerpValuesHandler.Instance.StartLerpByType(LerpValueConfig, EffectValues[0].ValueType, CurrentStartValue, CurrentEndValue, SetValue,OnTransitionEnd);
        }

        public override void EndTransitionEffect()
        {
            LerpValuesHandler.Instance.StartLerpByType(LerpValueConfig, EffectValues[0].ValueType, CurrentEndValue, CurrentStartValue, SetValue);
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
}