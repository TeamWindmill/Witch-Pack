using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform enviromentHolder;
    [SerializeField] private Transform shamanHolder;
    private List<Shaman> _shamanParty;

    private Shaman _shamanPrefab; //connect shaman prefab
    private LevelHandler _currentLevel;

    private void Start()
    {
        var levelConfig = GameManager.Instance.CurrentLevelConfig;
        _currentLevel = Instantiate(levelConfig.levelPrefab, enviromentHolder);
        GameManager.Instance.CameraHandler.SetCameraLevelSettings(levelConfig.CameraLevelSettings);
        GameManager.Instance.CameraHandler.ResetCamera();
        SpawnParty(levelConfig.Shamans);

    }

    private void SpawnParty(ShamanConfig[] shamanConfigs)
    {
        foreach (var shamanConfig in shamanConfigs)
        {
            var shaman = Instantiate(_shamanPrefab, shamanHolder);
            //init shaman with config
            _shamanParty.Add(shaman);
        }
        
    }
}
