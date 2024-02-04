using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnergyHandler 
{
    public event Action<int> OnShamanLevelUp;
    public event Action<int,int> OnShamanGainEnergy;
    
    public int ShamanLevel => _shamanLevel;
    public int AvailableSkillPoints => _shamanLevel - _usedSkillPoints;
    public int MaxEnergyToNextLevel => _energyLevels[_shamanLevel-1];
    public int CurrentEnergy => _currentEnergy;
    public bool HasSkillPoints => AvailableSkillPoints > 0;

    private int _shamanLevel = 1;
    private int _currentEnergy;
    private int[] _energyLevels;
    private int _usedSkillPoints = 1;
        
    public void Init(ShamanConfig config)
    {
        _energyLevels = new[]
        {
            config.EnergyLevels.Level1,
            config.EnergyLevels.Level2,
            config.EnergyLevels.Level3,
            config.EnergyLevels.Level4,
            config.EnergyLevels.Level5,
            config.EnergyLevels.Level6,
            config.EnergyLevels.MaxLevel
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
            return true;
        }

        return false;
    }
    
    private void LevelUp()
    {
        _shamanLevel++;
        if(_shamanLevel != 7) _currentEnergy = 0;
        OnShamanLevelUp?.Invoke(_shamanLevel);
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
}
