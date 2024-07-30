using System;
using System.Collections.Generic;
using Configs;
using External_Assets.PathCreator.Core.Runtime.Objects;
using Gameplay.Level;
using Gameplay.Pools.Pool_System;
using Gameplay.Units.Enemy;
using Gameplay.Wave.Indicator;
using Managers;
using UnityEngine;
using Visual.Indicator;

namespace Gameplay.Wave
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private PathCreator path;
        [SerializeField] private Indicatable indicateable; 
        private Queue<Enemy> queuedEnemies = new Queue<Enemy>();
        private Coroutine activeSpawningRoutine;
        private float lastSpawned = -0.4f;
        public Visual.Indicator.Indicator SetIndicator(Sprite artwork ,float time, Action onClick)
        {
            indicateable.Init(artwork, onClick, time, true, true, IndicatorPointerSpriteType.Red);
            Visual.Indicator.Indicator newIndicator = LevelManager.Instance.IndicatorManager.CreateIndicator(indicateable);
            return newIndicator;
        }

        public Enemy SpawnEnemy(EnemyConfig givenConf,LevelConfig levelConfig)
        {
            Enemy enemy = PoolManager.GetPooledObject<Enemy>();
            enemy.transform.position = transform.position;
            enemy.gameObject.SetActive(true);
            givenConf.Path = path;
            enemy.Init(givenConf);

            if (levelConfig.SelectedChallenge.ChallengeType is LevelChallengeType.AffectEnemies or LevelChallengeType.AffectBoth)
            {
                enemy.AddStatUpgrades(levelConfig.SelectedChallenge.StatUpgrades);
                enemy.Damageable.Init();
            }
        
            queuedEnemies.Enqueue(enemy);
            return enemy;
        }


        //private void Update()
        //{
        //    if (queuedEnemies.Count > 0 && GAME_TIME.TimePlayed - lastSpawned >= 0.4f)
        //    {
        //        queuedEnemies.Dequeue().gameObject.SetActive(true);
        //        lastSpawned = Time.time;
        //    }
        //}

    
    }
}
