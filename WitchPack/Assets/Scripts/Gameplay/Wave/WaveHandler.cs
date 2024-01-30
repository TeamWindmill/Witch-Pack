using DamageNumbersPro;
using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private PopupsManager popupsManager;


    public void Init()
    {
        spawnData = new List<EnemySpawnData>();
        foreach (EnemySpawnData item in waveData.waves)
        {
            EnemySpawnData newWave = new EnemySpawnData();
            newWave.Groups = new List<EnemyGroup>();

            newWave.Groups.AddRange(item.Groups);

            newWave.TimeBetweenIntervals = item.TimeBetweenIntervals;
            newWave.CalcSpawns();

            spawnData.Add(newWave);
        }
        StartCoroutine(StartSpawningWaves());
    }

    private IEnumerator StartSpawningWaves()
    {
        //yield return StartCoroutine(IntervalDelay(waveData.StartDelayInterval));
        //create start indicator 
        SetIndicator(0);
        yield return new WaitUntil(() => skipFlag);
        EnemySpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        for (int i = 0; i < spawnData.Count; i++)
        {
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
                SetIndicator(i, waveData.BetweenWavesInterval);
                yield return StartCoroutine(IntervalDelay(waveData.BetweenWavesInterval));
            }
        }
        LevelManager.Instance.EndLevel(true);
    }


    private IEnumerator SpawnWave(EnemySpawnData givenData)
    {
        doneSpawningCounter = 0;
        for (int i = 0; i < givenData.TotalSpawns; i++) //loop over how many spawns there are in total
        {
            for (int j = 0; j < givenData.Groups.Count; j++)
            {
                if (givenData.Groups[j].SpawnedAtInterval <= i + 1)
                {
                    StartCoroutine(SpawnGroupInterval(givenData.Groups[j]));
                }
            }

            yield return new WaitUntil(() => doneSpawningCounter >= givenData.Groups.Count * (i + 1));
            yield return StartCoroutine(IntervalDelay(givenData.TimeBetweenIntervals));
        }
    }

    private IEnumerator SpawnGroupInterval(EnemyGroup givenGroup)
    {
        for (int z = 0; z < givenGroup.AmountPerSpawn; z++)
        {
            if (givenGroup.TotalAmount <= givenGroup.NumSpawned) //if ran out of enemies to spawn break out.
            {
                break;
            }

            Enemy spawnedEnemy = GetSpawnPointFromIndex(givenGroup.SpawnerIndex).SpawnEnemy(givenGroup.Enemy);
            spawnedEnemy.Damageable.OnDamageCalc += popupsManager.SpawnDamagePopup;
            EnemyGroup group = givenGroup;
            group.NumSpawned++;
            givenGroup = group;

            yield return StartCoroutine(IntervalDelay(fixedSpawnInterval));
        }
        doneSpawningCounter++;
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
            if (skipFlag)
            {
                skipFlag = false;
                break;
            }
            counter += GAME_TIME.GameDeltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void SetIndicator(int waveIndex, float time = 0)
    {
        GetSpawnPointFromIndex(spawnData[waveIndex].Groups[0].SpawnerIndex).SetIndicator(spawnData[waveIndex].Groups[0].Enemy.UnitIcon, time, SkipWaveCD);
    }

    private void SkipWaveCD()
    {
        skipFlag = true;
    }

}