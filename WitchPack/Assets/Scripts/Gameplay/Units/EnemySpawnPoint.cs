using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{

    [SerializeField] private float spawningInterval;
    [SerializeField] private Enemy enemyPrefab; // for now

    public float SpawningInterval { get => spawningInterval; }

    public void SpawnEnemy(EnemyConfig givenConf)
    {
        Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.transform.parent = transform;// for now
        enemy.Init(givenConf);
    }



}
