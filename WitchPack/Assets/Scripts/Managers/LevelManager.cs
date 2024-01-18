using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] private Transform enviromentHolder;
    [SerializeField] private Transform shamanHolder;
    [SerializeField] private Shaman shamanPrefab;
    [SerializeField] private PartyUIManager partyUIManager;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private SelectionManager selectionManager;
    [SerializeField] private IndicatorManager indicatorManager;
    [SerializeField] private Canvas gameUi;
    [SerializeField] private EndScreenUIHandler endScreenUI;

    public LevelHandler CurrentLevel { get; private set; }
    public List<Shaman> ShamanParty { get; private set; }
    public bool IsWon { get; private set; }

    public SelectionManager SelectionManager
    {
        get => selectionManager;
    }

    public IndicatorManager IndicatorManager
    {
        get => indicatorManager;
    }

    public Canvas GameUi
    {
        get => gameUi;
    }

    public PoolManager PoolManager
    {
        get => poolManager;
    }

    private void Start()
    {
        var levelConfig = GameManager.Instance.CurrentLevelConfig;
        CurrentLevel = Instantiate(levelConfig.levelPrefab, enviromentHolder);
        CurrentLevel.Init();
        SpawnParty(levelConfig.Shamans);
        CurrentLevel.TurnOffSpawnPoints();
        UIManager.Instance.InitUIElements(UIGroup.GameUI);
        BgMusicManager.Instance.PlayMusic();
    }

    public void EndLevel(bool win)
    {
        IsWon = win;
        endScreenUI.Show();
        GAME_TIME.Pause();
    }

    private void SpawnParty(ShamanConfig[] shamanConfigs)
    {
        ShamanParty = new List<Shaman>();
        if (shamanConfigs.Length > CurrentLevel.ShamanSpawnPoints.Length)
        {
            Debug.LogError("there are more shamans than spawn points");
            return;
        }

        foreach (var shamanConfig in shamanConfigs)
        {
            int rand = Random.Range(0, CurrentLevel.ShamanSpawnPoints.Length);
            var spawnPoint = CurrentLevel.ShamanSpawnPoints[rand];

            while (!spawnPoint.gameObject.activeSelf)
            {
                rand = Random.Range(0, CurrentLevel.ShamanSpawnPoints.Length);
                spawnPoint = CurrentLevel.ShamanSpawnPoints[rand];
            }

            var shaman = Instantiate(shamanPrefab, spawnPoint.position, Quaternion.identity, shamanHolder);
            shaman.Init(shamanConfig);
            ShamanParty.Add(shaman);
            shaman.Damageable.OnDeath += RemoveShamanFromParty;
            spawnPoint.gameObject.SetActive(false);
        }
    }

    private void RemoveShamanFromParty(Damageable arg1, DamageDealer arg2, DamageHandler arg3, BaseAbility arg4)
    {
        if (arg1.Owner is Shaman shaman)
        {
            ShamanParty.Remove(shaman);
            if (ShamanParty.Count <= 0) EndLevel(false);
        }
    }
}