using System;
using TMPro;
using UnityEngine;

namespace Gameplay.Units.Energy_Exp
{
    public class PartyEnergyHandler : MonoBehaviour
    {
        public static Action<int> OnPartyLevelUp;
        public static Action<int, int> OnPartyGainEnergy;
        public static int CurrentEnergy { get; private set; }
        public static int PartyLevel { get; private set; }
        public static int AvailableSkillPoints => (PartyLevel - 1) * Config.SkillPointsPerLevel - _usedSkillPoints;
        public static int MaxEnergyToNextLevel => _levelsEnergyValues[PartyLevel - 1];
        public static bool HasSkillPoints => AvailableSkillPoints > 0;
        
        [SerializeField] private TextMeshProUGUI _partyEnergyText;
        [SerializeField] private EnergyConfig _config;
        
        private static int _usedSkillPoints;
        private static int[] _levelsEnergyValues => Config.LevelsEnergyValues;

        private static EnergyConfig Config;
        private static TextMeshProUGUI PartyEnergyText;

        private void Awake()
        {
            PartyLevel = 1;
            Config = _config;
            PartyEnergyText = _partyEnergyText;
            SetEnergyAmount(0);
        }

        public static void AddEnergy(int value)
        {
            SetEnergyAmount(CurrentEnergy + value);
            OnPartyGainEnergy?.Invoke(CurrentEnergy, MaxEnergyToNextLevel);
        }

        public static bool TryUseEnergyPoints(int energy)
        {
            if(CurrentEnergy >= energy)
            {
                SetEnergyAmount(CurrentEnergy - energy);
                return true;
            }
            return false;
        }

        private static void SetEnergyAmount(int value)
        {
            CurrentEnergy = value;
            PartyEnergyText.text = value.ToString();
        }
    }
}