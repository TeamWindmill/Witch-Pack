using System.Collections;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    [SerializeField] private EnemySpawnPoint[] spawnPoints;
    private WaveData waveData;//wave data is supposed to be given to a room from the inspector in the editor and not in runtime

   /* public void Init(WaveData givenWaveData)
    {
        waveData = givenWaveData;
    }
    private IEnumerator StartSpawningWaves()
    {
        EnemySpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        for (int i = 0; i < waveData.SpawnData.Length; i++)
        {
            yield return StartCoroutine(SpawnWave(waveData.SpawnData[i]));
            yield return StartCoroutine(IntervalDelay(waveData.WaveInterval));
        }
    }


    private IEnumerator SpawnWave(EnemySpawnData givenData)
    {
        EnemySpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        for (int i = 0; i < givenData.Enemies.Length; i++)
        {
            spawnPoint.SpawnEnemy(givenData.Enemies[i]);//basically how it should work?
            yield return StartCoroutine(IntervalDelay(givenData.SpwanInterval));
        }
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator IntervalDelay(float givenInterval)
    {
        float counter = 0f;
        while (counter < givenInterval)
        {
            counter += Time.deltaTime *//* * game time thing*//*; //for slow/ speed up time effect
            yield return new WaitForEndOfFrame();
        }
    }*/


    //wave data -> interval between waves, number of enemies of each type in each wave, interval between sapwns, 

}
