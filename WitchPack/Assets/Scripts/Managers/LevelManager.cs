using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoSingleton<LevelManager>
{
    public event Action<LevelHandler> OnLevelStart;
    public event Action<LevelHandler> OnLevelEnd;
    public LevelHandler CurrentLevel { get; private set; }
    public List<Shaman> ShamanParty { get; private set; }
    public bool IsWon { get; private set; }
    public ScoreHandler ScoreHandler => _scoreHandler;
    public ISelection SelectionHandler => _selectionManager.ActiveSelectionHandler;
    public IndicatorManager IndicatorManager => indicatorManager;
    public Canvas GameUi => gameUi;
    public PoolManager PoolManager => poolManager;
    public PopupsManager PopupsManager => popupsManager;

    [SerializeField] private Transform enviromentHolder;
    [SerializeField] private Transform shamanHolder;
    [SerializeField] private Shaman shamanPrefab;
    [SerializeField] private PartyUIManager partyUIManager;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private IndicatorManager indicatorManager;
    [SerializeField] private Canvas gameUi;
    [SerializeField] private PopupsManager popupsManager;
    [SerializeField] private SelectionManager _selectionManager;
    
    private readonly ScoreHandler _scoreHandler = new ScoreHandler();

    private void Start()
    {
        var levelConfig = GameManager.Instance.CurrentLevelConfig;
        CurrentLevel = Instantiate(levelConfig.levelPrefab, enviromentHolder);
        CurrentLevel.Init(levelConfig);
        SpawnParty(levelConfig.SelectedShamans);
        CurrentLevel.TurnOffSpawnPoints();
        BgMusicManager.Instance.PlayMusic(MusicClip.GameMusic);
        UIManager.Instance.ShowUIGroup(UIGroup.TopCounterUI);
        UIManager.Instance.ShowUIGroup(UIGroup.PartyUI);
        GAME_TIME.StartGame();
        TutorialHandler.Instance.LevelStart(CurrentLevel);
        OnLevelStart?.Invoke(CurrentLevel);
    }

    public void EndLevel(bool win)
    {
        IsWon = win;
        GAME_TIME.Pause();
        BgMusicManager.Instance.StopMusic();
        if (win)
        {
            SoundManager.Instance.PlayAudioClip(SoundEffectType.Victory);
            GameManager.Instance.ShamansManager.AddShamanToRoster(CurrentLevel.Config.shamansToAddAfterComplete);
            GameManager.SaveData.MapNodes[CurrentLevel.ID - 1].SetState(NodeState.Completed);
            GameManager.SaveData.LastLevelCompletedIndex = CurrentLevel.ID - 1;
        }
        UIManager.Instance.ShowUIGroup(UIGroup.EndGameUI);
        OnLevelEnd?.Invoke(CurrentLevel);
            
    }

    private void SpawnParty(List<ShamanSaveData> shamans)
    {
        ShamanParty = new List<Shaman>();
        if (shamans.Count > CurrentLevel.ShamanSpawnPoints.Length)
        {
            Debug.LogError("there are more shamans than spawn points");
            return;
        }

        foreach (var shamanSaveData in shamans)
        {
            int rand = Random.Range(0, CurrentLevel.ShamanSpawnPoints.Length);
            var spawnPoint = CurrentLevel.ShamanSpawnPoints[rand];

            while (!spawnPoint.gameObject.activeSelf)
            {
                rand = Random.Range(0, CurrentLevel.ShamanSpawnPoints.Length);
                spawnPoint = CurrentLevel.ShamanSpawnPoints[rand];
            }

            var shaman = Instantiate(shamanPrefab, spawnPoint.position, Quaternion.identity, shamanHolder);
            shaman.Init(shamanSaveData);
            ShamanParty.Add(shaman);
            shaman.Damageable.OnDeath += RemoveShamanFromParty;
            shaman.DamageDealer.OnKill += OnEnemyKill;
            spawnPoint.gameObject.SetActive(false);
        }
    }

    private void OnEnemyKill(Damageable arg1, DamageDealer arg2, DamageHandler arg3, Ability arg4, bool crit)
    {
        _scoreHandler.UpdateScore(kills: 1);
    }

    private void RemoveShamanFromParty(Damageable arg1, DamageDealer arg2)
    {
        if (arg1.Owner is Shaman shaman)
        {
            shaman.DamageDealer.OnKill -= OnEnemyKill;
            ShamanParty.Remove(shaman);
            if (ShamanParty.Count <= 0)
            {
                GameManager.Instance.CameraHandler.SetCameraPosition(shaman.transform.position,true);
                TimerManager.AddTimer(2, false, EndLevel);
            }
        }
    }
}