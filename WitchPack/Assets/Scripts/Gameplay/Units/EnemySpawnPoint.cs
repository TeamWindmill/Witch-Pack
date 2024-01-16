using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{

    [SerializeField] private PathCreator path;

    public void SpawnEnemy(EnemyConfig givenConf)
    {
        Enemy enemy = LevelManager.Instance.PoolManager.EnemyPool.GetPooledObject();
        enemy.transform.position = transform.position;
        enemy.gameObject.SetActive(true);
        enemy.Init(givenConf);
        enemy.SetPath(path);
    }



}
