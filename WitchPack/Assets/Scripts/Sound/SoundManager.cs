using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private bool _testing;
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private SoundsConfig _soundsConfig;
    private Dictionary<SoundEffectCategory, SoundEffect[]> _soundEffects;


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
        var category = GetCategoryBySound(soundEffectType);
        if (_soundEffects.TryGetValue(category,out var soundEffects))
        {
            foreach (var soundEffect in soundEffects)
            {
                if (soundEffect.Type == soundEffectType)
                {
                    audioClip = soundEffect.ClipVariations ? soundEffect.Clips[Random.Range(0,soundEffect.Clips.Length)] : soundEffect.Clip;
                    if (soundEffect.VolumeVariations) volume = Random.Range(soundEffect.VolumeValues.x,soundEffect.VolumeValues.y);
                    break;
                }
            }
        }
       
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
    public SoundEffectCategory GetCategoryBySound(SoundEffectType soundEffectType)
    {
        switch (soundEffectType)
        {
            case SoundEffectType.EnemyGetHit:
            case SoundEffectType.EnemyGetHitCrit:
            case SoundEffectType.EnemyDeath:
            case SoundEffectType.EnemyAttack:
            case SoundEffectType.EnemyWalk:
                return SoundEffectCategory.Enemy;
            case SoundEffectType.ShamanGetHitMale:
            case SoundEffectType.ShamanGetHitFemale:
            case SoundEffectType.ShamanDeathMale:
            case SoundEffectType.ShamanDeathFemale:
            case SoundEffectType.ShamanCast:
            case SoundEffectType.ShamanLevelUp:
                return SoundEffectCategory.Shaman;
            case SoundEffectType.BasicAttack:
            case SoundEffectType.PiercingShot:
            case SoundEffectType.MultiShot:
            case SoundEffectType.RootingVines:
            case SoundEffectType.Heal:
            case SoundEffectType.SmokeBomb:
            case SoundEffectType.Charm:
                return SoundEffectCategory.Abilities;
            case SoundEffectType.CoreGetHit:
            case SoundEffectType.CoreDestroyed:
                return SoundEffectCategory.Core;
            case SoundEffectType.UpgradeAbility:
            case SoundEffectType.OpenUpgradeTree:
            case SoundEffectType.MenuClick:
            case SoundEffectType.Victory:
                return SoundEffectCategory.UI;
            default:
                throw new ArgumentOutOfRangeException(nameof(soundEffectType), soundEffectType, null);
        }
    }

    private void OnValidate()
    {
        if (_audioSources == null || _audioSources.Length == 0)
        {
            _audioSources = GetComponents<AudioSource>();
        }
    }
}

