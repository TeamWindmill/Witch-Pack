using System;
using Sirenix.OdinInspector;

public class ExperienceHandler
{
    public event Action<int> OnShamanLevelUp;
    public event Action<int,int> OnShamanGainExp;
    public event Action<bool> OnShamanUpgrade;
    public int ShamanLevel { get; private set; } = 1;
    public int CurrentExp { get; private set; }
    public int AvailableSkillPoints => ShamanLevel - _usedSkillPoints;
    public int MaxExpToNextLevel => _shamanLevels[ShamanLevel-1];
    public bool HasSkillPoints => AvailableSkillPoints > 0;
    private int _usedSkillPoints;
    private int[] _shamanLevels;

    public ExperienceHandler(ExperienceConfig config)
    {
        _shamanLevels = config.LevelValues;
    }

    public void GainExp(int value)
    {
        if(ShamanLevel >= _shamanLevels.Length - 1) return;
        CurrentExp += value;
        if (CurrentExp >= MaxExpToNextLevel) LevelUp(CurrentExp - MaxExpToNextLevel);
        OnShamanGainExp?.Invoke(CurrentExp,MaxExpToNextLevel);
    }
    [Button]
    public void ManualExpGain()
    {
        GainExp(100);
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

    private void LevelUp(int excessExp)
    {
        ShamanLevel++;
        CurrentExp = 0;
        OnShamanLevelUp?.Invoke(ShamanLevel);
        GainExp(excessExp);
    }
}