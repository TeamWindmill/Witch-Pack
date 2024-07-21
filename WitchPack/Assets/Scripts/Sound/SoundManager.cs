using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private bool _testing;
    [SerializeField] private int _startingAudioSourcesAmount;
    [SerializeField] private AudioSource _highPrioPrefab;
    [SerializeField] private AudioSource _lowPrioPrefab;
    [SerializeField] private SoundsConfig _soundsConfig;
    
    private static List<AudioSource> _highPriorityAudioSources = new();
    private static List<AudioSource> _lowPriorityAudioSources = new();
    private static Dictionary<SoundEffectCategory, SoundEffect[]> _soundEffects;
    protected override void Awake()
    {
        base.Awake();
        _soundEffects = _soundsConfig.SoundEffects;
        for (int i = 0; i < _startingAudioSourcesAmount; i++)
        {
            CreateAudioSource(true);
        }

        for (int i = 0; i < _startingAudioSourcesAmount; i++)
        {
            CreateAudioSource(false);
        }
    }

    private static AudioSource CreateAudioSource(bool highPrio)
    {
        AudioSource source;
        if (highPrio)
        {
            source = Instantiate(Instance._highPrioPrefab, Instance.transform);
            _highPriorityAudioSources.Add(source);
        }
        else
        {
            source = Instantiate(Instance._lowPrioPrefab, Instance.transform);
            _lowPriorityAudioSources.Add(source);
        }
        return source;
    }

    public static void PlayAudioClip(SoundEffectType soundEffectType, float pitch = 1, float volume = 1, bool loop = false)
    {
        SoundEffect soundEffect = GetSoundEffect(soundEffectType);

        if (soundEffect is null)
        {
            Debug.LogWarning("did not find any matching sound effect in SoundManager");
            return;
        }
        
        #region audioClip
        
        AudioClip audioClip = soundEffect.ClipVariations ? soundEffect.ProvideRandomClip() : soundEffect.Clip;
        if (soundEffect.VolumeVariations) volume = Random.Range(soundEffect.VolumeValues.x, soundEffect.VolumeValues.y);

        if (ReferenceEquals(audioClip, null))
        {
#if UNITY_EDITOR
            if(Instance._testing)
            {
                var title = ColorLogHelper.SetColorToString("SOUND:", Color.cyan);
                Debug.Log($"{title} Playing {soundEffectType} Sound Effect");
            }
            else Debug.LogWarning("did not find any matching audio clips in sound handler");
#endif

            return;
        }
        #endregion

        #region audioSource

        AudioSource audioSource = null;

        if (soundEffect.HighPriority)
        {
            foreach (var source in _highPriorityAudioSources)
            {
                if (source.isPlaying) continue;
        
                audioSource = source;
                break;
            }

            audioSource ??= CreateAudioSource(true);
        }
        else
        {
            foreach (var source in _lowPriorityAudioSources)
            {
                if (source.isPlaying) continue;
        
                audioSource = source;
                break;
            }

            audioSource ??= CreateAudioSource(false);
        }
        
        if (ReferenceEquals(audioSource, null))
        {
            Debug.LogError("Error with audio source pooling");
            return;
        }
        #endregion

        audioSource.clip = audioClip;
        audioSource.loop = loop;
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.Play();
    }

    private static SoundEffect GetSoundEffect(SoundEffectType soundEffectType)
    {
        var category = GetCategoryBySound(soundEffectType);
        if (_soundEffects.TryGetValue(category, out var soundEffects))
        {
            foreach (var effect in soundEffects)
            {
                if (effect.Type == soundEffectType)
                {
                    return effect;
                }
            }
        }

        return null;
    }
    private static SoundEffectCategory GetCategoryBySound(SoundEffectType soundEffectType)
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
            case SoundEffectType.HighImpactSmokeBomb:
            case SoundEffectType.Charm:
            case SoundEffectType.OverHeal:
            case SoundEffectType.HealingWeeds:
            case SoundEffectType.Frenzy:
            case SoundEffectType.PoisonIvy:
            case SoundEffectType.BlessingOfSwiftness:
            case SoundEffectType.InspiredSmokeBomb:
            case SoundEffectType.ExperiencedHunterLevelUp:
            case SoundEffectType.MultiShotRicochet:
            case SoundEffectType.MonolithofRockExplosion:
            case SoundEffectType.MonolithofRockShield:
            case SoundEffectType.MonolithofRockTaunt:
            case SoundEffectType.OrbitalStonesCreating:
            case SoundEffectType.OrbitalStonesDestruction:
            case SoundEffectType.ReflectDamage:
                return SoundEffectCategory.Abilities;
            case SoundEffectType.CoreGetHit:
            case SoundEffectType.CoreDestroyed:
                return SoundEffectCategory.Core;
            case SoundEffectType.UpgradeAbility:
            case SoundEffectType.OpenUpgradeTree:
            case SoundEffectType.MenuClick:
            case SoundEffectType.Victory:
            case SoundEffectType.LockedAbility:
                return SoundEffectCategory.UI;
            default:
                Debug.LogError($"{soundEffectType} not in a category");
                return SoundEffectCategory.Enemy;
        }
    }
}