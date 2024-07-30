using System;
using Tools.Lerp;
using UnityEngine;

namespace Tutorial
{
    [Serializable]
    public class CanvasLerper : EffectTransitionLerp
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public void SetStartValue()
        {
            _canvasGroup.alpha = EffectValue.StartValue;
            _canvasGroup.gameObject.SetActive(false);
        }
    
        protected override void SetValue(float value)
        {
            _canvasGroup.alpha = value;
        }
        public override void StartTransitionEffect()
        {
            _canvasGroup.gameObject.SetActive(true);
            base.StartTransitionEffect();
        }
        protected override void OnTransitionEnd(float newValue)
        {
            if (newValue == 0)
            {
                _canvasGroup.gameObject.SetActive(false);
            }
        }


    }
}