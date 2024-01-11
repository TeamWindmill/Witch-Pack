using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Wave")]
public class WaveData : ScriptableObject
{
    [SerializeField] private float waveInterval;//the interval between the end a wave to the beginning of the next one 
    public List<EnemySpawnData> waves = new List<EnemySpawnData>();

    public float WaveInterval { get => waveInterval; }
    //public List<EnemySpawnData> Waves { get => waves; set => waves = value; }

    [Button]
    public void CalcSpawns()
    {
        for (int i = 0; i < waves.Count; i++)
        {
            waves[i].CalcSpawns();
        }
    }

}

[Serializable]
public class EnemySpawnData
{
    [ReadOnly] public int TotalSpawns;
    public float TimeBetweenIntervals;
    public List<EnemyGroup> Groups;

    public int CalcSpawns()
    {
        int highestDivision = Groups[0].GetNumberOfSpawns();
        foreach (var group in Groups)
        {
            if (group.GetNumberOfSpawns() > highestDivision)
            {
                highestDivision = group.GetNumberOfSpawns();
            }
        }

        TotalSpawns = highestDivision;
        return highestDivision;
    }

}

[Serializable]
public struct EnemyGroup
{
    public EnemyConfig Enemy;
    public int AmountPerSpawn;
    public int TotalAmount;
    public int SpawnedAtInterval;//this is the spawn interval this group will start spawaning at
    [ReadOnly] public int NumSpawned;


    public int GetNumberOfSpawns()
    {
        return (TotalAmount / AmountPerSpawn) + (TotalAmount % AmountPerSpawn) + (SpawnedAtInterval - 1);
    }

}
