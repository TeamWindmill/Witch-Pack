using System;
using System.Collections.Generic;
using Dialog;
using Gameplay.Level;
using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities;
using Gameplay.Units.Damage_System;
using Gameplay.Units.Energy_Exp;
using Gameplay.Units.Selection;
using Gameplay.Units;
using Map;
using Sound;
using GameTime;
using Tools.Helpers;
using Tools.Time;
using Tutorial;
using UI.GameUI;
using UI.GameUI.EndScreen;
using UI.GameUI.HeroSelectionUI;
using UI.GameUI.PartyUI;
using UI.UISystem;
using UnityEngine;
using Visual.Indicator;
using Random = UnityEngine.Random;

namespace Managers
{
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
        public PopupsManager PopupsManager => popupsManager;

        [SerializeField] private Transform enviromentHolder;
        [SerializeField] private Transform shamanHolder;
        [SerializeField] private Shaman shamanPrefab;
        [SerializeField] private GamePartyUIPanel _gamePartyUIPanel;
        [SerializeField] private IndicatorManager indicatorManager;
        [SerializeField] private Canvas gameUi;
        [SerializeField] private PopupsManager popupsManager;
        [SerializeField] private SelectionManager _selectionManager;

        private readonly ScoreHandler _scoreHandler = new ScoreHandler();

        private void Start()
        {
            var levelConfig = GameManager.CurrentLevelConfig;
            CurrentLevel = Instantiate(levelConfig.levelPrefab, enviromentHolder);
            CurrentLevel.Init(levelConfig,GameManager.SaveData.LevelSaves[GameManager.SaveData.CurrentNode.Index]);
            SpawnParty(levelConfig.SelectedShamans);
            CurrentLevel.TurnOffSpawnPoints();
            if (levelConfig.StartDialog != null)
            {
                DialogBox.Instance.SetDialogSequence(levelConfig.StartDialog, StartLevel);
                DialogBox.Instance.Show();
            }
            else StartLevel();
        }

        private void StartLevel()
        {
            CurrentLevel.StartLevel();
            BgMusicManager.Instance.PlayMusic(MusicClip.GameMusic);
            UIManager.ShowUIGroup(UIGroup.TopCounterUI);
            UIManager.ShowUIGroup(UIGroup.PartyUI);
            GAME_TIME.StartGame();
            TutorialHandler.Instance.LevelStart(CurrentLevel);
            OnLevelStart?.Invoke(CurrentLevel);
        }

        public void EndLevel(bool win)
        {
            IsWon = win;
            StartEndLevelSequence();
        }

        private void StartEndLevelSequence()
        {
            GAME_TIME.Pause();
            HeroSelectionUI.Instance.Hide();
            GameManager.CameraHandler.ToggleCameraLock(true);
            if (IsWon)
            {
                if (CurrentLevel.Config.EndDialog != null)
                {
                    DialogBox.Instance.SetDialogSequence(CurrentLevel.Config.EndDialog,FinishEndLevelSequence);
                    DialogBox.Instance.Show();
                }
                else FinishEndLevelSequence();
            }
            else FinishEndLevelSequence();
        }

        private void FinishEndLevelSequence()
        {
            GiveExpToParty();
            if (IsWon)
            {
                SoundManager.PlayAudioClip(SoundEffectType.Victory);
                GameManager.ShamansManager.AddShamanToRoster(CurrentLevel.Config.shamansToAddAfterComplete);
                GameManager.SaveData.LevelSaves[CurrentLevel.ID - 1].State = NodeState.Completed;
                GameManager.SaveData.LevelSaves[CurrentLevel.ID - 1].ChallengesFirstTimes[CurrentLevel.Config.SelectedChallenge.Index] = false;
                GameManager.SaveData.LastLevelCompletedIndex = CurrentLevel.ID - 1;
            }

            BgMusicManager.Instance.StopMusic();
            UIManager.ShowUIGroup(UIGroup.EndGameUI);
            OnLevelEnd?.Invoke(CurrentLevel);
        }

        private void GiveExpToParty()
        {
            var levelData = new EndLevelStats(
                completed: IsWon,
                firstTime: CurrentLevel.LevelSaveData.ChallengesFirstTimes[CurrentLevel.Config.SelectedChallenge.Index],
                coreRemainingHp: CurrentLevel.CoreTemple.Damageable.CurrentHp /  CurrentLevel.CoreTemple.Damageable.MaxHp,
                wavesCompletedPercentage: (float)(CurrentLevel.WaveHandler.CurrentWave - 1) / CurrentLevel.WaveHandler.TotalWaves,
                expMultiplier: CurrentLevel.Config.SelectedChallenge.ExpMultiplier
            );
            var expGained = LevelExpCalculator.CalculateExpGainedFromLevel(CurrentLevel.Config.ExpCalculatorConfig, levelData);
            foreach (var shaman in ShamanParty)
            {
                shaman.SaveData.ShamanExperienceHandler.GainExp(expGained);
                Debug.Log($"{shaman.ShamanConfig.Name} Gained {expGained} Exp");
            }
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
                var spawnPoint = GetSpawnPoint();

                var shaman = Instantiate(shamanPrefab, spawnPoint.position, Quaternion.identity, shamanHolder);
                shaman.Init(shamanSaveData);
                ShamanParty.Add(shaman);
            
                if(CurrentLevel.Config.SelectedChallenge.ChallengeType is LevelChallengeType.AffectShamans or LevelChallengeType.AffectBoth) 
                    shaman.AddStatUpgrades(CurrentLevel.Config.SelectedChallenge.StatUpgrades);
            
                shaman.Damageable.OnDeath += RemoveShamanFromParty;
                shaman.DamageDealer.OnKill += OnEnemyKill;
                spawnPoint.gameObject.SetActive(false);
            }
        }

        private Transform GetSpawnPoint()
        {
            int rand = Random.Range(0, CurrentLevel.ShamanSpawnPoints.Length);
            var spawnPoint = CurrentLevel.ShamanSpawnPoints[rand];

            while (!spawnPoint.gameObject.activeSelf)
            {
                rand = Random.Range(0, CurrentLevel.ShamanSpawnPoints.Length);
                spawnPoint = CurrentLevel.ShamanSpawnPoints[rand];
            }

            return spawnPoint;
        }

        private void OnEnemyKill(Damageable arg1, DamageDealer arg2, DamageHandler arg3, Ability arg4, bool crit)
        {
            _scoreHandler.UpdateScore(kills: 1);
        }

        private void RemoveShamanFromParty(Damageable arg1, DamageDealer arg2) //no exp for dead shamans
        {
            if (arg1.Owner is Shaman shaman)
            {
                shaman.DamageDealer.OnKill -= OnEnemyKill;
                ShamanParty.Remove(shaman);
                if (ShamanParty.Count <= 0)
                {
                    GameManager.CameraHandler.SetCameraPosition(shaman.transform.position, true);
                    TimerManager.AddTimer(2, false, EndLevel);
                }
            }
        }
    }
}