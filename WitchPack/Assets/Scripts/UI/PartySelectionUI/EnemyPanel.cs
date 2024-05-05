using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
//using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyPanel : UIElement
{
    [SerializeField] private EnemyIcon _enemyIconPrefab;
    [SerializeField] private Transform _holder;

    private Dictionary<EnemyConfig, int> _enemiesInLevel;
    private List<EnemyIcon> _enemyIcons = new();

    public void Init(LevelConfig levelConfig,EnemyPanelConfig enemyPanelConfig)
    {
        Hide();
        _enemiesInLevel = new();
        foreach (var enemySpawnData in levelConfig.levelPrefab.WaveHandler.WaveData.waves)
        {
            foreach (var enemyGroup in enemySpawnData.Groups)
            {
                if (_enemiesInLevel.ContainsKey(enemyGroup.Enemy))
                    _enemiesInLevel[enemyGroup.Enemy] += enemyGroup.TotalAmount;
                else _enemiesInLevel[enemyGroup.Enemy] = enemyGroup.TotalAmount;
            }
        }

        foreach (var enemyType in _enemiesInLevel)
        {
            foreach (var config in enemyPanelConfig.EnemyAmounts)
            {
                if (enemyType.Value > config.AmountRange.x && enemyType.Value < config.AmountRange.y)
                {
                    EnemyIcon enemyIcon = Instantiate(_enemyIconPrefab, _holder);
                    enemyIcon.Init(enemyType.Key, config.AmountName);
                    _enemyIcons.Add(enemyIcon);
                }
            }

            if (enemyType.Value > enemyPanelConfig.AmountThreshold)
            {
                EnemyIcon enemyIcon = Instantiate(_enemyIconPrefab, _holder);
                enemyIcon.Init(enemyType.Key, enemyPanelConfig.AmountName);
                _enemyIcons.Add(enemyIcon);
            }
        }
    }

    public override void Hide()
    {
        if (_enemyIcons.Count > 0)
        {
            foreach (var enemyIcon in _enemyIcons)
            {
                Destroy(enemyIcon.gameObject);
            }
            _enemyIcons.Clear();
        }
    }
}

[Serializable]
public struct EnemyPanelConfig
{
    public EnemyAmountDisplayConfig[] EnemyAmounts;
    [BoxGroup("Threshold")] public string AmountName;
    [BoxGroup("Threshold")] public int AmountThreshold;
}
[Serializable]
public struct EnemyAmountDisplayConfig
{
    public string AmountName;
    [MinMaxSlider(1,500)]public Vector2Int AmountRange;
}
