using System;
using DG.Tweening;
using UnityEngine;


[Serializable]
public class AudioFilters : EffectTransitionLerp<AudioFilterValueType>
{
    protected AudioSource _audioSource;
    protected AudioReverbFilter _audioReverbFilter;
    protected AudioLowPassFilter _audioLowPassFilter;

    public void Init(AudioSource audioSource, AudioReverbFilter audioReverbFilter, AudioLowPassFilter audioLowPassFilter)
    {
        _audioSource = audioSource;
        _audioReverbFilter = audioReverbFilter;
        _audioLowPassFilter = audioLowPassFilter;
    }

    protected override void SetValue(AudioFilterValueType type, float value)
    {
        switch (type)
        {
            case AudioFilterValueType.cutoffFrequency:
                _audioLowPassFilter.cutoffFrequency = value;
                break;
            case AudioFilterValueType.lowpassResonanceQ:
                _audioLowPassFilter.lowpassResonanceQ = value;
                break;
            case AudioFilterValueType.revebLevel:
                _audioReverbFilter.reverbLevel = value;
                break;
            case AudioFilterValueType.dryLevel:
                _audioReverbFilter.dryLevel = value;
                break;
            case AudioFilterValueType.pitch:
                _audioSource.pitch = value;
                break;
        }
    }
    public void StartTransition()
    {
        foreach (var audioEffect in EffectValues)
        {
            switch (audioEffect.ValueType)
            {
                case AudioFilterValueType.dryLevel:
                    DOTween.To(() => _audioReverbFilter.dryLevel, x => _audioReverbFilter.dryLevel = x,audioEffect.EndValue , LerpValueConfig.TransitionTime);
                    break;
                case AudioFilterValueType.revebLevel:
                    DOTween.To(() => _audioReverbFilter.reverbLevel, x => _audioReverbFilter.reverbLevel = x,audioEffect.EndValue , LerpValueConfig.TransitionTime);
                    break;
                case AudioFilterValueType.lowpassResonanceQ:
                    DOTween.To(() => _audioLowPassFilter.lowpassResonanceQ, x => _audioLowPassFilter.lowpassResonanceQ = x,audioEffect.EndValue , LerpValueConfig.TransitionTime);
                    break;
                case AudioFilterValueType.cutoffFrequency:
                    DOTween.To(() => _audioLowPassFilter.cutoffFrequency, x => _audioLowPassFilter.cutoffFrequency = x,audioEffect.EndValue , LerpValueConfig.TransitionTime);
                    break;
                case AudioFilterValueType.pitch:
                    DOTween.To(() => _audioSource.pitch, x => _audioSource.pitch = x,audioEffect.EndValue , LerpValueConfig.TransitionTime);
                    break;
            }
        }
    }

    public void EndTransition()
    {
        foreach (var audioEffect in EffectValues)
        {
            switch (audioEffect.ValueType)
            {
                case AudioFilterValueType.dryLevel:
                    DOTween.To(() => _audioReverbFilter.dryLevel, x => _audioReverbFilter.dryLevel = x,audioEffect.StartValue , LerpValueConfig.TransitionTime);
                    break;
                case AudioFilterValueType.revebLevel:
                    DOTween.To(() => _audioReverbFilter.reverbLevel, x => _audioReverbFilter.reverbLevel = x,audioEffect.StartValue , LerpValueConfig.TransitionTime);
                    break;
                case AudioFilterValueType.lowpassResonanceQ:
                    DOTween.To(() => _audioLowPassFilter.lowpassResonanceQ, x => _audioLowPassFilter.lowpassResonanceQ = x,audioEffect.StartValue , LerpValueConfig.TransitionTime);
                    break;
                case AudioFilterValueType.cutoffFrequency:
                    DOTween.To(() => _audioLowPassFilter.cutoffFrequency, x => _audioLowPassFilter.cutoffFrequency = x,audioEffect.StartValue , LerpValueConfig.TransitionTime);
                    break;
                case AudioFilterValueType.pitch:
                    DOTween.To(() => _audioSource.pitch, x => _audioSource.pitch = x,audioEffect.StartValue , LerpValueConfig.TransitionTime);
                    break;
            }
        }
    }
}

public enum AudioFilterValueType
{
    dryLevel = 0,
    revebLevel = 1,
    lowpassResonanceQ = 2,
    cutoffFrequency = 3,
    pitch = 4
}