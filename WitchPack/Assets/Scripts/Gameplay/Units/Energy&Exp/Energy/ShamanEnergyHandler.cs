using System;
using Gameplay.Units.Abilities.AbilitySystem.AbilityStats;
using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities;
using Gameplay.Units.Abilities.AbilitySystem.Casting;
using Gameplay.Units.Damage_System;
using Gameplay.Units.Stats;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Units.Energy_Exp
{
    [Serializable]
    public class ShamanEnergyHandler 
    {
        public event Action<int> OnShamanLevelUp;
        public int ShamanLevel { get; private set; } = 1;

        private int _usedSkillPoints = 1;
        private Shaman _shaman;
        private EnergyConfig _config;

        public ShamanEnergyHandler(Shaman shaman)
        {
            _shaman = shaman;
            _config = shaman.ShamanConfig.EnergyConfig;
        }
        private void LevelUp()
        {
            ShamanLevel++;
            OnShamanLevelUp?.Invoke(ShamanLevel);
            LevelManager.Instance.PopupsManager.SpawnLevelUpTextPopup(_shaman);
        }
    }
}
