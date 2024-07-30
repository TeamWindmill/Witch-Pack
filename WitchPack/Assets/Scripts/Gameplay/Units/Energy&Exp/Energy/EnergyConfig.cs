using UnityEngine;

namespace Gameplay.Units.Energy_Exp
{
    [CreateAssetMenu(fileName = "EnergyConfig", menuName = "EnergyConfig")]
    public class EnergyConfig : ScriptableObject
    {
        [Header("Energy")] 
        //[SerializeField,Range(0,100)] private float _assistPercent;
        [SerializeField] private int _skillPointsPerLevel;
        [SerializeField] private int[] levelsEnergyValues;
        

        public int[] LevelsEnergyValues => levelsEnergyValues;
        //public float AssistPercent => _assistPercent/100;
        public int SkillPointsPerLevel => _skillPointsPerLevel;
    }
}
