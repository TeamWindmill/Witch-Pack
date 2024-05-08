using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShamansManager
{
    [SerializeField] private List<ShamanConfig> _startingShamanRoster;

    public List<ShamanSaveData> ShamanRoster { get; private set; }

    private GameSaveData _gameSaveData;

    public void Init(GameSaveData saveData)
    {
        _gameSaveData = saveData;
        if (saveData.ShamanRoster == null)
        {
            List<ShamanSaveData> shamansSaveData = new();
            _startingShamanRoster.ForEach((config) => shamansSaveData.Add(new ShamanSaveData(config)));
            ShamanRoster = shamansSaveData;
            saveData.ShamanRoster = ShamanRoster;
        }
        else
        {
            ShamanRoster = saveData.ShamanRoster;
        }
    }
    
    public void AddShamanToRoster(ShamanConfig shamanConfig)
    {
        ShamanRoster.Add(new ShamanSaveData(shamanConfig));
    }
    public void AddShamanToRoster(ShamanConfig[] shamansConfig)
    {
        foreach (var config in shamansConfig)
        {
            ShamanRoster.Add(new ShamanSaveData(config));
        }
    }
}