using System;
using DG.Tweening;
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
    public void StartTransition()
    {
        foreach (var postProcessEffect in EffectValues)
        {
            switch (postProcessEffect.ValueType)
            {
                case PostProcessType.Bloom:
                    if (_postProcessVolume.profile.TryGet<Bloom>(out var bloom))
                    {
                        DOTween.To(() => bloom.intensity.value, x => bloom.intensity.value = x,postProcessEffect.EndValue , LerpValueConfig.TransitionTime);
                    }
                    break;
                case PostProcessType.Vignette:
                    if (_postProcessVolume.profile.TryGet<Vignette>(out var vignette))
                    {
                        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x,postProcessEffect.EndValue , LerpValueConfig.TransitionTime);
                    }
                    break;
                case PostProcessType.ColorAdjustments:
                    if (_postProcessVolume.profile.TryGet<ColorAdjustments>(out var colorAdjustments))
                    {
                        DOTween.To(() => colorAdjustments.saturation.value, x => colorAdjustments.saturation.value = x,postProcessEffect.EndValue , LerpValueConfig.TransitionTime);
                    }
                    break;
            }
        }
    }

    public void EndTransition()
    {
        foreach (var postProcessEffect in EffectValues)
        {
            switch (postProcessEffect.ValueType)
            {
                case PostProcessType.Bloom:
                    if (_postProcessVolume.profile.TryGet<Bloom>(out var bloom))
                    {
                        DOTween.To(() => bloom.intensity.value, x => bloom.intensity.value = x,postProcessEffect.StartValue , LerpValueConfig.TransitionTime);
                    }
                    break;
                case PostProcessType.Vignette:
                    if (_postProcessVolume.profile.TryGet<Vignette>(out var vignette))
                    {
                        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x,postProcessEffect.StartValue , LerpValueConfig.TransitionTime);
                    }
                    break;
                case PostProcessType.ColorAdjustments:
                    if (_postProcessVolume.profile.TryGet<ColorAdjustments>(out var colorAdjustments))
                    {
                        DOTween.To(() => colorAdjustments.saturation.value, x => colorAdjustments.saturation.value = x,postProcessEffect.StartValue , LerpValueConfig.TransitionTime);
                    }
                    break;
            }
        }
    }
}

public enum PostProcessType
{
    Bloom,
    Vignette,
    ColorAdjustments
}