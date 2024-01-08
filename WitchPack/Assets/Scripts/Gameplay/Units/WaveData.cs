using Sirenix.OdinInspector;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Wave")]
public class WaveData : ScriptableObject
{
    [SerializeField] private float waveInterval;//the interval between the end a wave to the beginning of the next one 
    [SerializeField] private EnemySpawnData[] waves;

    public EnemySpawnData[] Waves { get => waves; }
    public float WaveInterval { get => waveInterval; }

    [Button]
    public void CalcSpawns()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            int highestDivision = waves[i].Groups[0].GetNumberOfSpawns();
            foreach (var group in waves[i].Groups)
            {
                if (group.GetNumberOfSpawns() > highestDivision)
                {
                    highestDivision = group.GetNumberOfSpawns();
                }
            }
            waves[i].totalSpawns = highestDivision;
        }
    }

}

[Serializable]
public struct EnemySpawnData
{
    [ReadOnly] public int totalSpawns;
    public float SpwanInterval;
    public EnemyGroup[] Groups;
}

[Serializable]
public struct EnemyGroup
{
    public EnemyConfig Enemy;
    public int AmountPerSpawn;
    public int TotalAmount;
    [Min(1)] public int FirstSpawn;//this is the spawn interval this group will start spawaning at

    public int GetNumberOfSpawns()
    {
        return (TotalAmount / AmountPerSpawn) + (TotalAmount % AmountPerSpawn) + (FirstSpawn - 1);
    }

}
