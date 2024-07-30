using System;
using Tools.Lerp;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameUI.ScreenCracksUI
{
    [Serializable]
    public class ScreenCrackLerper : EffectTransitionLerp
    {
        public bool Finished { get; private set; }
        public Image Image => _image;

        [SerializeField] private Image _image;

        public void SetImage(Image image)
        {
            _image = image;
        }

        public void SetStartValue()
        {
            _image.material.SetFloat("_Offset_X", EffectValue.StartValue);
            Finished = false;
        }

        protected override void SetValue(float value)
        {
            _image.material.SetFloat("_Offset_X", value);
        }

        protected override void OnTransitionEnd(float newValue)
        {
            if (newValue == EffectValue.EndValue) Finished = true;
        }
    }
}