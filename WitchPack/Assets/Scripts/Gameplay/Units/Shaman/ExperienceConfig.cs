using UnityEngine;

[CreateAssetMenu(menuName = "ExperienceConfig",fileName = "ExperienceConfig")]
public class ExperienceConfig : ScriptableObject
{
    [SerializeField] private int[] levelValues;

    public int[] LevelValues => levelValues;
}
