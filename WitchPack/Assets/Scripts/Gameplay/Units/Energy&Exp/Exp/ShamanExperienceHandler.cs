using System;
using Sirenix.OdinInspector;

namespace Gameplay.Units.Energy_Exp.Exp
{
    public class ShamanExperienceHandler
    {
        public event Action<int> OnShamanLevelUp;
        public event Action<int, int> OnShamanGainExp;
        public event Action<bool> OnShamanUpgrade;
        public int ShamanLevel { get; private set; } = 1;
        public int CurrentExp { get; private set; }
        public int AvailableSkillPoints => (ShamanLevel - 1) * _config.SkillPointsPerLevel - _usedSkillPoints;
        public int MaxExpToNextLevel => _shamanLevels[ShamanLevel - 1];
        public bool HasSkillPoints => AvailableSkillPoints > 0;
        private int _usedSkillPoints;
        private int[] _shamanLevels;
        private ShamanExperienceConfig _config;

        public ShamanExperienceHandler(ShamanExperienceConfig config)
        {
            _shamanLevels = config.LevelValues;
            _config = config;
        }

        public void GainExp(int value)
        {
            if (ShamanLevel >= _shamanLevels.Length - 1) return;
            CurrentExp += value;
            if (CurrentExp >= MaxExpToNextLevel) LevelUp(CurrentExp - MaxExpToNextLevel);
            OnShamanGainExp?.Invoke(CurrentExp, MaxExpToNextLevel);
        }

        [Button]
        public void ManualExpGain()
        {
            GainExp(100);
        }

        public void UseSkillPoints(int value)
        {
            _usedSkillPoints += value;
        }
        public void ResetSkillPoints()
        {
            _usedSkillPoints = 0;
        }

        private void LevelUp(int excessExp)
        {
            ShamanLevel++;
            CurrentExp = 0;
            OnShamanLevelUp?.Invoke(ShamanLevel);
            GainExp(excessExp);
        }
    }
}