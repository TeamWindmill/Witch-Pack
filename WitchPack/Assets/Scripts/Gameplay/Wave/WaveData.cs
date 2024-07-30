using System;
using System.Collections.Generic;
using Configs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Wave
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "Level/Wave")]
    public class WaveData : ScriptableObject
    {
        [SerializeField] private float startDelayInterval;//the interval between the end a wave to the beginning of the next one 
        [SerializeField] private float betweenWavesInterval;//the interval between the end a wave to the beginning of the next one 
        public List<EnemySpawnData> waves = new List<EnemySpawnData>();



        public float BetweenWavesInterval { get => betweenWavesInterval; }
        public float StartDelayInterval { get => startDelayInterval; }
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

        public int GetTotalNumberOfEnemies()
        {
            int total = 0;
            for (int i = 0; i < Groups.Count; i++)
            {
                total += Groups[i].TotalAmount;
            }
            return total;
        }

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
    public class EnemyGroup
    {
        public EnemyConfig Enemy;
        public int AmountPerSpawn;
        public int TotalAmount;
        public int SpawnedAtInterval;//this is the spawn interval this group will start spawaning at
        [Tooltip("this refers to the index of the spawn point the group is intended to spawn at." +
                 " If the number is higher than the highest index the spawner will be set to the highest index")]
        public int SpawnerIndex;
        [ReadOnly] public int NumSpawned;

        private bool completedSpawning;
        public bool CompletedSpawning { get => completedSpawning; set => completedSpawning = value; }

        public EnemyGroup(EnemyConfig givenEnemy, int amountPerSpawn, int totalAmount, int spawnInterval, int spawnerIndex)
        {
            Enemy = givenEnemy;
            AmountPerSpawn = amountPerSpawn;
            TotalAmount = totalAmount;
            SpawnedAtInterval = spawnInterval;
            SpawnerIndex = spawnerIndex;
        }

        public int GetNumberOfSpawns()
        {
            return (TotalAmount / AmountPerSpawn) + (TotalAmount % AmountPerSpawn) + (Mathf.Clamp(SpawnedAtInterval - 1, 0, SpawnedAtInterval));
        }

    }
}