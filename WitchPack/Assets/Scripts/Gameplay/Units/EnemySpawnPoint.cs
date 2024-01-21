using System.Collections;
using System;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private PathCreator path;
    [SerializeField] private Indicatable indicateable; 
    public void SetIndicator(Sprite artwork ,float time, Action onClick)
    {
        indicateable.Init(artwork, onClick, time, true);
        LevelManager.Instance.IndicatorManager.CreateIndicator(indicateable);
    }

    public void SpawnEnemy(EnemyConfig givenConf)
    {
        Enemy enemy = LevelManager.Instance.PoolManager.EnemyPool.GetPooledObject();
        enemy.transform.position = transform.position;
        enemy.gameObject.SetActive(true);
        givenConf.Path = path;
        enemy.Init(givenConf);
    }



}
