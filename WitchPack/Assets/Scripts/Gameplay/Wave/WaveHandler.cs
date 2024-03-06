using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveHandler : MonoBehaviour
{
    public event Action<int> OnWaveStart;
    public event Action<int> OnWaveEnd;



    [SerializeField] private EnemySpawnPoint[] spawnPoints;
    [SerializeField] private WaveData waveData; //wave data is supposed to be given to a room from the inspector in the editor and not in runtime
    [SerializeField] private float fixedSpawnInterval;
    private List<EnemySpawnData> spawnData;
    private int _currentWave;
    private bool skipFlag;
    private int doneSpawningCounter;
    public int CurrentWave => _currentWave;
    public int TotalWaves => spawnData.Count;


    public void Init()
    {
        spawnData = new List<EnemySpawnData>();
        foreach (EnemySpawnData item in waveData.waves)
        {
            EnemySpawnData newWave = new EnemySpawnData();
            newWave.Groups = new List<EnemyGroup>();

            foreach (var group in item.Groups)
            {
                newWave.Groups.Add(new EnemyGroup(group.Enemy, group.AmountPerSpawn, group.TotalAmount, group.SpawnedAtInterval, group.SpawnerIndex));
            }
            

            newWave.TimeBetweenIntervals = item.TimeBetweenIntervals;
            newWave.CalcSpawns();

            spawnData.Add(newWave);
        }
        StartCoroutine(StartSpawningWaves());
    }

    private IEnumerator StartSpawningWaves()
    {
        //yield return StartCoroutine(IntervalDelay(waveData.StartDelayInterval));
        int nextWave = 0;
        //create start indicator 
        SetIndicator(nextWave);
        yield return new WaitUntil(() => skipFlag);        
        for (int i = 0; i < spawnData.Count; i++)
        {
            nextWave = i + 1;
            // Spawning Wave
            Debug.Log("Spawning Wave " + i);
            spawnData[i].CalcSpawns();
            OnWaveStart?.Invoke(i + 1);
            _currentWave = i + 1;
            yield return StartCoroutine(SpawnWave(spawnData[i]));//wait until done spawning
            OnWaveEnd?.Invoke(i + 1);
            if (i == spawnData.Count - 1)//if on last wave finished spawning wait until everything dies 
            {
                yield return new WaitUntil(() => !LevelManager.Instance.PoolManager.EnemyPool.CheckActiveIstance());
            }
            else // if a wave other than the last one finished spawning set up the next wave. 
            {
                Debug.Log("Showing Indicators for Wave " + nextWave);
                SetIndicator(nextWave, waveData.BetweenWavesInterval);
                yield return StartCoroutine(WaveDelay(waveData.BetweenWavesInterval));
            }
        }
        LevelManager.Instance.EndLevel(true);
    }


    //private IEnumerator SpawnWave(EnemySpawnData givenData)
    //{
    //    for (int i = 0; i < givenData.TotalSpawns; i++) //loop over how many spawns there are in total
    //    {
    //        for (int j = 0; j < givenData.Groups.Count; j++)
    //        {
    //            if(i >= givenData.Groups[j].SpawnedAtInterval)
    //            {
    //                StartCoroutine(SpawnGroup(givenData.Groups[j]));
    //            }
    //        }

    //        yield return StartCoroutine(IntervalDelay(givenData.TimeBetweenIntervals));
    //    }
    //}

    private IEnumerator SpawnWave(EnemySpawnData givenData)
    {
        bool waveCompletedSpawning = false;
        int currentInterval = 0;

        foreach (EnemyGroup group in givenData.Groups)
        {
            group.CompletedSpawning = false;
        }

        while (!waveCompletedSpawning)
        {
            waveCompletedSpawning = true;

            for (int i = 0; i < givenData.Groups.Count; i++)
            {
                if (givenData.Groups[i].CompletedSpawning)
                {
                    continue; // no need to spawn any more
                }
                else
                {
                    waveCompletedSpawning = false;
                }

                if (currentInterval >= givenData.Groups[i].SpawnedAtInterval)
                {
                    StartCoroutine(SpawnGroupInterval(givenData.Groups[i]));
                }
            }

            currentInterval++;
            yield return StartCoroutine(IntervalDelay(givenData.TimeBetweenIntervals));
        }
    }

    private IEnumerator SpawnGroupInterval(EnemyGroup givenGroup)
    {
        EnemySpawnPoint spawnPoint = GetSpawnPointFromIndex(givenGroup.SpawnerIndex);
        for (int z = 0; z < givenGroup.AmountPerSpawn; z++)
        {
            if (givenGroup.TotalAmount <= givenGroup.NumSpawned) //if ran out of enemies to spawn break out.
            {
                break;
            }           
            spawnPoint.SpawnEnemy(givenGroup.Enemy);
            givenGroup.NumSpawned++;

            yield return StartCoroutine(IntervalDelay(fixedSpawnInterval));
        }

        if (givenGroup.TotalAmount <= givenGroup.NumSpawned) //if ran out of enemies to spawn break out.
        {
            givenGroup.CompletedSpawning = true;
        }
    }


    private EnemySpawnPoint GetSpawnPointFromIndex(int index)
    {
        if (index >= spawnPoints.Length)
        {
            return spawnPoints[spawnPoints.Length - 1];
        }
        else if (index <= 0)
        {
            return spawnPoints[0];
        }
        else
        {
            return spawnPoints[index - 1];
        }
    }

    private IEnumerator IntervalDelay(float givenInterval)
    {
        float counter = 0f;
        while (counter < givenInterval)
        {
            counter += GAME_TIME.GameDeltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator WaveDelay(float givenInterval)
    {
        float counter = 0f;
        while (counter < givenInterval)
        {
            if (skipFlag)
            {
                skipFlag = false;
                break; //caused first enemy in the first wave to spawn on top of the second enemy
            }
            counter += GAME_TIME.GameDeltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void SetIndicator(int waveIndex, float time = 0)
    {
        List<int> lanesThatHaveAnIndicator = new List<int>();
        foreach (EnemyGroup group in spawnData[waveIndex].Groups)
        {
            if (lanesThatHaveAnIndicator.Contains(group.SpawnerIndex))
            {
                continue; // Skips setting an indicator on a lane that already has an indicator
            }
            lanesThatHaveAnIndicator.Add(group.SpawnerIndex);
            EnemySpawnPoint spawnPoint = GetSpawnPointFromIndex(group.SpawnerIndex);
            spawnPoint.SetIndicator(group.Enemy.UnitIcon, time, SkipWaveCD);
        }
        
    }

    private void SkipWaveCD()
    {
        skipFlag = true;
    }

}