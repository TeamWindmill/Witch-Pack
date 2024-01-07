using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] private Transform enviromentHolder;
    [SerializeField] private Transform shamanHolder;
    [SerializeField] private Shaman shamanPrefab;
    [SerializeField] private PartyUIManager partyUIManager;

    public LevelHandler CurrentLevel { get; private set; }
    public List<Shaman> ShamanParty { get; private set; }
    public static bool IsWon { get; private set; }

    private void Start()
    {
        var levelConfig = GameManager.Instance.CurrentLevelConfig;
        CurrentLevel = Instantiate(levelConfig.levelPrefab, enviromentHolder);
        GameManager.Instance.CameraHandler.SetCameraLevelSettings(levelConfig.CameraLevelSettings);
        GameManager.Instance.CameraHandler.ResetCamera();
        SpawnParty(levelConfig.Shamans);
        CurrentLevel.TurnOffSpawnPoints();
        partyUIManager.Init(ShamanParty);
        BgMusicManager.Instance.PlayMusic();
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
            spawnPoint.gameObject.SetActive(false);
        }
    }
}