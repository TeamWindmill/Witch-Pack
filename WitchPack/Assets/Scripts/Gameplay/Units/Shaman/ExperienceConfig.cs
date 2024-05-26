using UnityEngine;

public class ExperienceConfig : ScriptableObject
{
    [SerializeField] private int[] levelValues;

    public int[] LevelValues => LevelValues;
}
