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
    private int deathCounter;
    public int CurrentWave => _currentWave;
    public int TotalWaves => spawnData.Count;


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
            yield return StartCoroutine(SpawnWave(spawnData[i], spawnPoint));
            OnWaveEnd?.Invoke(i + 1);
            SetIndicator(i, waveData.BetweenWavesInterval);
            if (i == spawnData.Count -1)
            {
                yield return new WaitUntil(() => deathCounter >= spawnData[i].GetTotalNumberOfEnemies());
            }
            yield return StartCoroutine(IntervalDelay(waveData.BetweenWavesInterval));
            deathCounter = 0;
        }
        LevelManager.Instance.EndLevel(true);
    }


    private IEnumerator SpawnWave(EnemySpawnData givenData, EnemySpawnPoint point)
    {
        for (int i = 0; i < givenData.TotalSpawns; i++) //loop over how many spawns there are in total
        {
            for (int j = 0; j < givenData.Groups.Count; j++)
            {
                EnemySpawnPoint currentSpawnPoint = GetSpawnPointFromIndex(givenData.Groups[j].SpawnerIndex);
                if (givenData.Groups[j].SpawnedAtInterval <= i + 1)
                {
                    for (int z = 0; z < givenData.Groups[j].AmountPerSpawn; z++)
                    {
                        if (givenData.Groups[j].TotalAmount <= givenData.Groups[j].NumSpawned) //if ran out of enemies to spawn break out.
                        {
                            break;
                        }

                        currentSpawnPoint.SpawnEnemy(givenData.Groups[j].Enemy).Damageable.OnDeath += DeathCounterIncrement;
                        EnemyGroup group = givenData.Groups[j];
                        group.NumSpawned++;
                        givenData.Groups[j] = group;

                        yield return StartCoroutine(IntervalDelay(fixedSpawnInterval));//for now
                    }
                }
            }

            yield return StartCoroutine(IntervalDelay(givenData.TimeBetweenIntervals));
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
            return spawnPoints[index-1];
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

    private void SetIndicator(int waveIndex, float time =0)
    {
        GetSpawnPointFromIndex(spawnData[waveIndex].Groups[0].SpawnerIndex).SetIndicator(spawnData[waveIndex].Groups[0].Enemy.UnitIcon, time, SkipWaveCD);
    }

    private void SkipWaveCD()
    {
        skipFlag = true;
    }

    private void DeathCounterIncrement(Damageable target, DamageDealer dealer, DamageHandler dmg, BaseAbility ability)
    {
        deathCounter++;
        target.OnDeath -= DeathCounterIncrement;
    }
}