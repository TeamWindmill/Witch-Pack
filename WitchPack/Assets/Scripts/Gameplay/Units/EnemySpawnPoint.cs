using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{

    [SerializeField] private CustomPath path;

    public void SpawnEnemy(EnemyConfig givenConf)
    {
        Enemy enemy = LevelManager.Instance.PoolManager.EnemyPool.GetPooledObject();
        enemy.transform.position = transform.position;
        enemy.gameObject.SetActive(true);
        enemy.SetPath(path);
        enemy.Init(givenConf);
    }



}
