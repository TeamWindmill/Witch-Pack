using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class EnergyHandler 
{
    public event Action<int> OnShamanLevelUp;
    public event Action<int,int> OnShamanGainEnergy;
    public event Action<bool> OnShamanUpgrade;
    
    public int ShamanLevel => _shamanLevel;
    public int AvailableSkillPoints => _shamanLevel - _usedSkillPoints;
    public int MaxEnergyToNextLevel => _energyLevels[_shamanLevel-1];
    public int CurrentEnergy => _currentEnergy;
    public bool HasSkillPoints => AvailableSkillPoints > 0;

    private int _shamanLevel = 1;
    private int _currentEnergy;
    private int[] _energyLevels;
    private int _usedSkillPoints = 1;
    private Shaman _shaman;
    private EnergyConfig _config;

    public EnergyHandler(Shaman shaman)
    {
        _shaman = shaman;
        _config = shaman.ShamanConfig.EnergyConfig;
        _energyLevels = new[]
        {
            _config.Level1,
            _config.Level2,
            _config.Level3,
            _config.Level4,
            _config.Level5,
            _config.Level6,
            _config.MaxLevel
        };
    }
    public void GainEnergy(int energy = 0)
    {
        if (energy == 0) energy = 25; //temp
        if(_shamanLevel == 7) return;
        _currentEnergy += energy;
        OnShamanGainEnergy?.Invoke(_currentEnergy,MaxEnergyToNextLevel);
        if(_currentEnergy >= MaxEnergyToNextLevel) LevelUp();
    }

    public bool TryUseSkillPoint()
    {
        if (AvailableSkillPoints > 0)
        {
            _usedSkillPoints++;
            OnShamanUpgrade?.Invoke(HasSkillPoints);
            return true;
        }

        return false;
    }
    
    private void LevelUp()
    {
        _shamanLevel++;
        if(_shamanLevel != 7) _currentEnergy = 0;
        OnShamanLevelUp?.Invoke(_shamanLevel);
        LevelManager.Instance.PopupsManager.SpawnLevelUpTextPopup(_shaman);
    }

    [Button]
    public void ManualGainEnergy()
    {
        GainEnergy(100);
    }

    public void OnEnemyKill(Damageable damageable, DamageDealer arg2, DamageHandler arg3, BaseAbility arg4, bool arg5)
    {
        var enemy = damageable.Owner as Enemy;
        if (enemy is null)
        {
            Debug.LogError("target not set as an enemy");
            return;
        }
        GainEnergy(enemy.EnergyPoints);
    }
    public void OnEnemyAssist(Damageable damageable, DamageDealer arg2, DamageHandler arg3, BaseAbility arg4, bool arg5)
    {
        var enemy = damageable.Owner as Enemy;
        if (enemy is null)
        {
            Debug.LogError("target not set as an enemy");
            return;
        }
        GainEnergy((int)(enemy.EnergyPoints * _config.AssistPercent)); //might change it to a different percent later on
    }
    public void OnShamanCast(AbilityCaster caster)
    {
        GainEnergy(caster.Ability.EnergyPoints); 
    }
}
