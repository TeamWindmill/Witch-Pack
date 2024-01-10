using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    [SerializeField] private EnemySpawnPoint[] spawnPoints;
    [SerializeField] private WaveData waveData;//wave data is supposed to be given to a room from the inspector in the editor and not in runtime
    [SerializeField] private List<EnemySpawnData> spawnData;


    public void Init()
    {
        foreach (EnemySpawnData item in waveData.waves)
        {
            EnemySpawnData newWave = new EnemySpawnData();
            newWave.Groups = new List<EnemyGroup>();

            newWave.Groups.AddRange(item.Groups);

            newWave.SpwanInterval = item.SpwanInterval;
            newWave.CalcSpawns();

            spawnData.Add(newWave);
        }


    }

    [Button]
    public void StartSpawning()
    {
        StartCoroutine(StartSpawningWaves());
    }

    private IEnumerator StartSpawningWaves()
    {
        EnemySpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        for (int i = 0; i < spawnData.Count; i++)
        {
            spawnData[i].CalcSpawns();
            yield return StartCoroutine(SpawnWave(spawnData[i], spawnPoint));
            yield return StartCoroutine(IntervalDelay(10f));//temp
        }
    }


    private IEnumerator SpawnWave(EnemySpawnData givenData, EnemySpawnPoint point)
    {
        for (int i = 0; i < givenData.TotalSpawns; i++)//loop over how many spawns there are in total
        {
            for (int j = 0; j < givenData.Groups.Count; j++)
            {
                if (givenData.Groups[j].SpawnedAtInterval <= i + 1)
                {
                    for (int z = 0; z < givenData.Groups[j].AmountPerSpawn; z++)
                    {
                        if (givenData.Groups[j].TotalAmount <= givenData.Groups[j].NumSpawned)//if ran out of enemies to spawn break out.
                        {
                            break;
                        }
                        point.SpawnEnemy(givenData.Groups[j].Enemy);

                        EnemyGroup group = givenData.Groups[j];
                        group.NumSpawned++;
                        givenData.Groups[j] = group;

                        yield return new WaitForSeconds(0.5f);//for now
                    }
                }
            }
            yield return StartCoroutine(IntervalDelay(givenData.SpwanInterval));
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
}
