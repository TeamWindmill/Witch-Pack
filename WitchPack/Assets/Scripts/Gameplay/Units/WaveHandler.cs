using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    [SerializeField] private EnemySpawnPoint[] spawnPoints;
    private WaveData waveData;

    public void Init(WaveData givenWaveData)
    {
        waveData = givenWaveData;
    }

    private IEnumerator SpawnWave()
    {
        EnemySpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        for (int i = 0; i < waveData.SpawnData.Length; i++)
        {
            for (int j = 0; j < waveData.SpawnData[i].Amount; j++)
            {
                spawnPoint.SpawnEnemy(waveData.SpawnData[i].enemyType);//basically how it should work?
            }
        }
        yield return new WaitForEndOfFrame();
    }

    

    //wave data -> interval between waves, number of enemies of each type in each wave, interval between sapwns, 

}
