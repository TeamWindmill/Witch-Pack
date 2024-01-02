using System;
using UnityEngine.Rendering.PostProcessing;


[Serializable]
public class PostProcessSlowMotionEffect : EffectTransitionLerp<PostProcessType>
{
    private PostProcessVolume _postProcessVolume;

    public bool IsInitialization { get; }

    public void Init(PostProcessVolume postProcessVolume)
    {
        _postProcessVolume = postProcessVolume;
    }

    protected override void SetValue(PostProcessType type, float value)
    {
        switch (type)
        {
            case PostProcessType.Bloom:
                try
                {
                    var bloom = _postProcessVolume.profile.GetSetting<Bloom>();
                    bloom.intensity.value = value;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                break;
            case PostProcessType.Vignette:
                try
                {
                    var vignette = _postProcessVolume.profile.GetSetting<Vignette>();
                    vignette.intensity.value = value;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                break;
        }
    }
}

public enum PostProcessType
{
    Bloom,
    Vignette
}