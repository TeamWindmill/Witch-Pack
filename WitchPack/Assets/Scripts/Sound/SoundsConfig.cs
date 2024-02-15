using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundConfig",menuName = "Sound/SoundConfig",order = 5)]
public class SoundsConfig : ScriptableObject
{
    public SoundEffect[] SoundEffects => _enemySoundEffects.Concat(_shamanSoundEffects.Concat(_abilitiesSoundEffects.Concat(_coreSoundEffects.Concat(_uiSoundEffects)))).ToArray();
    
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
    public AudioClip Clip;
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