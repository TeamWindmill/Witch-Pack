using System;
using UnityEngine;

[Serializable]
public class WaterfallEffectHandler : EffectTransitionLerp<AnimatorSlowMotionEffectType>
{
    private Animator _waterfallAnimator;
    
    public void Init(Animator waterfallAnimator)
    {
        _waterfallAnimator = waterfallAnimator;
    }

    protected override void SetValue(AnimatorSlowMotionEffectType type, float value)
    {
        switch (type)
        {
            case AnimatorSlowMotionEffectType.speed:
                _waterfallAnimator.speed = value;
                break;
        }
    }
}

public enum AnimatorSlowMotionEffectType
{
    speed,
}