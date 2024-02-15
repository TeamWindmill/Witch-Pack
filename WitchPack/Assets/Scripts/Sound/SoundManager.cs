using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;


public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private bool _testing;
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private SoundsConfig _soundsConfig;
    private Dictionary<SoundEffectCategory, SoundEffect[]> _soundEffects;

    private Random _random = new Random();

    private void Start()
    {
        _soundEffects = _soundsConfig.SoundEffects;
    }

    public void PlayAudioClip(SoundEffectType soundEffectType, float pitch = 1, float volume = 1, bool loop = false)
    {
        AudioSource audioSource = null;

        foreach (var source in _audioSources)
        {
            if (source.isPlaying) continue;

            audioSource = source;
            break;
        }

        if (ReferenceEquals(audioSource,null))
        {
            Debug.LogWarning("did not find any available audio source");
            return;
        }

        AudioClip audioClip = null;
        // GetSoundsByCategory(_soundEffects);
        // foreach (var soundEffect in _soundEffects)
        // {
        //     if (soundEffect.Type == soundEffectType)
        //     {
        //         if(soundEffect.Variations)
        //             audioClip = soundEffect.Clips[_random.Next(0,soundEffect.Clips.Length)];
        //         else
        //             audioClip = soundEffect.Clip;
        //         break;
        //     }
        // }

        if (ReferenceEquals(audioClip,null))
        {
            if (_testing)
            {
                var title = ColorLogHelper.SetColorToString("SOUND:", Color.cyan);
                Debug.Log($"{title} Playing {soundEffectType} Sound Effect");
            }
            else Debug.LogWarning("did not find any matching audio clips in sound handler");
            return;
        }

        audioSource.clip = audioClip;
        audioSource.loop = loop;
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.Play();
    }
    // public SoundEffect[] GetCategoryBySound(SoundEffectType soundEffectType)
    // {
    //     if (_soundEffects.TryGetValue(category, out var value))
    //     {
    //         return value;
    //     }
    //     Debug.LogError("sound category not found");
    //     return null;
    // }

    private void OnValidate()
    {
        if (_audioSources == null || _audioSources.Length == 0)
        {
            _audioSources = GetComponents<AudioSource>();
        }
    }
}

