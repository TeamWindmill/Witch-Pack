using System;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;


public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private bool _testing;
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private SoundEffect[] _soundEffects;

    public void PlayAudioClip(SoundEffectType soundEffectType, float pitch = 1, float volume = 1, bool loop = false)
    {
        AudioSource audioSource = null;

        foreach (var source in _audioSources)
        {
            if (source.isPlaying) continue;

            audioSource = source;
            break;
        }

        if (audioSource == null)
        {
            Debug.LogWarning("did not find any available audio source");
            return;
        }

        AudioClip audioClip = null;
        foreach (var soundEffect in _soundEffects)
        {
            if (soundEffect.Type != soundEffectType) continue;
            audioClip = soundEffect.Clip;
            break;
        }

        if (audioClip == null)
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

    private void OnValidate()
    {
        if (_audioSources == null || _audioSources.Length == 0)
        {
            _audioSources = GetComponents<AudioSource>();
        }
    }
}

[Serializable]
public struct SoundEffect
{
    public SoundEffectType Type;
    public AudioClip Clip;
}

public enum SoundEffectType
{
    //Projectile Sounds
    BasicAttack,
    CriticalAttack, //?
    
    //Enemies
    EnemyGetHit,
    EnemyGetHitCrit,
    EnemyDeath,
    EnemyAttack,
    EnemyWalk, //?
    
    //Shamans
    ShamanGetHit, //with male and female?
    ShamanGetHitCrit,
    ShamanDeathMale,
    ShamanDeathFemale,
    ShamanCast,
    
    //Abilities
    PiercingShot,
    RootingVines,
    SmokeBomb,
    Heal,
    
}