using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform enviromentHolder;
    [SerializeField] private Transform shamanHolder;
    private LevelConfig _levelConfig;
    private List<Shaman> _shamanParty;

    private Shaman shamanPrefab; //connect shaman prefab

    private void Start()
    {
        _levelConfig = GameManager.Instance.CurrentLevelConfig;
        Instantiate(_levelConfig.LevelMap, enviromentHolder);
        GameManager.CameraHandler.SetCameraLevelSettings(_levelConfig.CameraLevelSettings);
        GameManager.CameraHandler.ResetCamera();
        SpawnParty(_levelConfig.Shamans);

    }

    private void SpawnParty(ShamanConfig[] shamanConfigs)
    {
        foreach (var shamanConfig in shamanConfigs)
        {
            var shaman = Instantiate(shamanPrefab, shamanHolder);
            //init shaman with config
            _shamanParty.Add(shaman);
        }
        
    }
}
