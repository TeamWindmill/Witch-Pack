using System;
using DG.Tweening;
using Tools.Lerp;
using UnityEngine;

namespace Tools.SlowMotion
{
    [Serializable]
    public class WaterfallEffectHandler 
    {
        private Animator _waterfallAnimator;
        [SerializeField] private LerpConfig<float> LerpConfig;
        public void Init(Animator waterfallAnimator)
        {
            _waterfallAnimator = waterfallAnimator;
        }

        public void StartTransition()
        {
            DOTween.To(() => _waterfallAnimator.speed, x => _waterfallAnimator.speed = x, LerpConfig.EndValue, LerpConfig.TransitionTime);
        }

        public void EndTransition()
        {
            DOTween.To(() => _waterfallAnimator.speed, x => _waterfallAnimator.speed = x, LerpConfig.StartValue, LerpConfig.TransitionTime);
        }
    }

    public enum AnimatorSlowMotionEffectType
    {
        speed,
    }
}