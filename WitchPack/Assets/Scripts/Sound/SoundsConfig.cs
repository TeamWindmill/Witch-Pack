using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundConfig",menuName = "Sound/SoundConfig",order = 5)]
public class SoundsConfig : ScriptableObject
{
    public Dictionary<SoundEffectCategory, SoundEffect[]> SoundEffects =>
        new()
        {
            { SoundEffectCategory.Enemy, _enemySoundEffects },
            { SoundEffectCategory.Shaman, _shamanSoundEffects },
            { SoundEffectCategory.Abilities, _abilitiesSoundEffects },
            { SoundEffectCategory.Core, _coreSoundEffects },
            { SoundEffectCategory.UI, _uiSoundEffects },
        };
    
    [SerializeField,TabGroup("Enemy")] private SoundEffect[] _enemySoundEffects;
    [SerializeField,TabGroup("Shaman")] private SoundEffect[] _shamanSoundEffects;
    [SerializeField,TabGroup("Abilities")] private SoundEffect[] _abilitiesSoundEffects;
    [SerializeField,TabGroup("Core")] private SoundEffect[] _coreSoundEffects;
    [SerializeField,TabGroup("UI")] private SoundEffect[] _uiSoundEffects;
}
[Serializable]
public struct SoundEffect
{
    public SoundEffectType Type;
    public bool ClipVariations;
    public bool VolumeVariations;
    [HideIf(nameof(ClipVariations))]public AudioClip Clip;
    [ShowIf(nameof(ClipVariations))]public AudioClip[] Clips;
    [ShowIf(nameof(VolumeVariations)),MinMaxSlider(0,1)]public Vector2 VolumeValues;
    private SoundEffectCategory _category;
}
public enum SoundEffectCategory
{
    Enemy,
    Shaman,
    Core,
    Abilities,
    UI
}
public enum SoundEffectType
{
    //Enemies
    EnemyGetHit,
    EnemyGetHitCrit,
    EnemyDeath,
    EnemyAttack,
    EnemyWalk, 
    
    //Shamans
    ShamanGetHitMale,
    ShamanGetHitFemale,
    ShamanDeathMale,
    ShamanDeathFemale,
    ShamanCast,
    ShamanLevelUp,
    
    //Abilities
    BasicAttack,
    PiercingShot,
    MultiShot,
    RootingVines,
    Heal,
    SmokeBomb,
    Charm,
    
    //core
    CoreGetHit,
    CoreDestroyed,
    
    //UI
    UpgradeAbility,
    OpenUpgradeTree,
    MenuClick,
    Victory,
}