using UnityEngine;

namespace Gameplay.Units.Energy_Exp
{
    [CreateAssetMenu(menuName = "ShamanExperienceConfig",fileName = "ShamanExperienceConfig")]
    public class ShamanExperienceConfig : ScriptableObject
    {
        [SerializeField] private int[] levelValues;
        [SerializeField] private int skillPointsPerLevel;

        public int[] LevelValues => levelValues;
        public int SkillPointsPerLevel => skillPointsPerLevel;
    }
}
