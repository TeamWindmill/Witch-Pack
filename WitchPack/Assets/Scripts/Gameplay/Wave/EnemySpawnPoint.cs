using System.Collections;
using System;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;
using TMPro;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private PathCreator path;
    [SerializeField] private Indicatable indicateable; 
    private Queue<Enemy> queuedEnemies = new Queue<Enemy>();
    private Coroutine activeSpawningRoutine;
    private float lastSpawned = -0.4f;
    public void SetIndicator(Sprite artwork ,float time, Action onClick)
    {
        indicateable.Init(artwork, onClick, time, true);
        LevelManager.Instance.IndicatorManager.CreateIndicator(indicateable);
    }

    public Enemy SpawnEnemy(EnemyConfig givenConf)
    {
        Enemy enemy = LevelManager.Instance.PoolManager.EnemyPool.GetPooledObject();
        enemy.transform.position = transform.position;
        //enemy.gameObject.SetActive(true);
        givenConf.Path = path;
        enemy.Init(givenConf);
        queuedEnemies.Enqueue(enemy);
        return enemy;
    }


    private void Update()
    {
        if (queuedEnemies.Count > 0 && GAME_TIME.TimePlayed - lastSpawned >= 0.4f)
        {
            queuedEnemies.Dequeue().gameObject.SetActive(true);
            lastSpawned = Time.time;
        }
    }

    
}
