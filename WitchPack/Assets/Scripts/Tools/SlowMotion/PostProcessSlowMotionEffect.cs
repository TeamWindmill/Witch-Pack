using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


[Serializable]
public class PostProcessSlowMotionEffect : EffectTransitionLerp<PostProcessType>
{
    private Volume _postProcessVolume;

    public void Init(Volume postProcessVolume)
    {
        _postProcessVolume = postProcessVolume;
    }

    protected override void SetValue(PostProcessType type, float value)
    {
        switch (type)
        {
            case PostProcessType.Bloom:
                if (_postProcessVolume.profile.TryGet<Bloom>(out var bloom))
                {
                    bloom.intensity.value = value;
                }
                break;
            case PostProcessType.Vignette:
                if (_postProcessVolume.profile.TryGet<Vignette>(out var vignette))
                {
                    vignette.intensity.value = value;
                }
                break;
            case PostProcessType.ColorAdjustments:
                if (_postProcessVolume.profile.TryGet<ColorAdjustments>(out var colorAdjustments))
                {
                    colorAdjustments.saturation.value = value;
                }
                break;
        }
    }
}

public enum PostProcessType
{
    Bloom,
    Vignette,
    ColorAdjustments
}