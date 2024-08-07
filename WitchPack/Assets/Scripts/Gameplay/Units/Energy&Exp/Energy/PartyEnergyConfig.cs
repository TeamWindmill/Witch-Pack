using UnityEngine;


    [CreateAssetMenu(fileName = "PartyEnergyConfig", menuName = "Energy/PartyEnergyConfig")]
    public class PartyEnergyConfig : ScriptableObject
    {
        [SerializeField] private int[] _levelsEnergyValues;
        [SerializeField] private int _skillPointsPerLevel;
        
        public int[] LevelsEnergyValues => _levelsEnergyValues;
        public int SkillPointsPerLevel => _skillPointsPerLevel;
    }
