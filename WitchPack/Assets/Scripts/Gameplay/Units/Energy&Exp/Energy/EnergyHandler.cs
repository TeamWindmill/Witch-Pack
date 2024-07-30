using System;
using Gameplay.Units.Abilities.AbilitySystem.AbilityStats;
using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities;
using Gameplay.Units.Abilities.AbilitySystem.Casting;
using Gameplay.Units.Damage_System;
using Gameplay.Units.Stats;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Units.Energy_Exp.Energy
{
    [Serializable]
    public class EnergyHandler 
    {
        public event Action<int> OnShamanLevelUp;
        public event Action<int,int> OnShamanGainEnergy;
        public event Action<bool> OnShamanUpgrade;
    
        public int ShamanLevel { get; private set; } = 1;
        public int CurrentEnergy { get; private set; }
        public int AvailableSkillPoints => ShamanLevel - _usedSkillPoints;
        public int MaxEnergyToNextLevel => _energyLevels[ShamanLevel-1];
        public bool HasSkillPoints => AvailableSkillPoints > 0;

        private int[] _energyLevels;
        private int _usedSkillPoints = 1;
        private Shaman.Shaman _shaman;
        private EnergyConfig _config;

        public EnergyHandler(Shaman.Shaman shaman)
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
        public void GainEnergy(float energy)
        {
            if(ShamanLevel == 7) return;
            CurrentEnergy += (int)(energy * _shaman.Stats[StatType.EnergyGainMultiplier].Value);
            if(CurrentEnergy >= MaxEnergyToNextLevel) LevelUp(CurrentEnergy - MaxEnergyToNextLevel);
            OnShamanGainEnergy?.Invoke(CurrentEnergy,MaxEnergyToNextLevel);
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
    
        private void LevelUp(int excessEnergy)
        {
            ShamanLevel++;
            if(ShamanLevel != 7) CurrentEnergy = 0;
            OnShamanLevelUp?.Invoke(ShamanLevel);
            LevelManager.Instance.PopupsManager.SpawnLevelUpTextPopup(_shaman);
            GainEnergy(excessEnergy);
        }

        [Button]
        public void ManualGainEnergy()
        {
            GainEnergy(100);
        }

        public void OnEnemyKill(Damageable damageable, DamageDealer arg2, DamageHandler arg3, Ability arg4, bool arg5)
        {
            var enemy = damageable.Owner as Enemy.Enemy;
            if (enemy is null)
            {
                Debug.LogError("target not set as an enemy");
                return;
            }
            GainEnergy(enemy.EnergyPoints);
        }
        public void OnEnemyAssist(Damageable damageable, DamageDealer arg2, DamageHandler arg3, Ability arg4, bool arg5)
        {
            var enemy = damageable.Owner as Enemy.Enemy;
            if (enemy is null)
            {
                Debug.LogError("target not set as an enemy");
                return;
            }
            GainEnergy((int)(enemy.EnergyPoints * _config.AssistPercent)); //might change it to a different percent later on
        }
        public void OnShamanCast(AbilityCaster caster,IDamagable target)
        {
            GainEnergy(caster.Ability.GetAbilityStatIntValue(AbilityStatType.EnergyPointsOnCast));
        }
    }
}
