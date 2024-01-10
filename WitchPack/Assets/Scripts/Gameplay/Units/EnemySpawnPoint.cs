using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{

    [SerializeField] private Enemy enemyPrefab; // for now
    [SerializeField] private CustomPath path;

    public void SpawnEnemy(EnemyConfig givenConf)
    {
        Enemy enemy = LevelManager.Instance.PoolManager.EnemyPool.GetPooledObject();
        enemy.transform.parent = transform;// for now
        enemy.SetPath(path);
        enemy.gameObject.SetActive(true);
        enemy.Init(givenConf);
    }



}
