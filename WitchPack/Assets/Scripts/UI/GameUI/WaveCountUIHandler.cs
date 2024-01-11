using System;
using TMPro;
using UnityEngine;

public class WaveCountUIHandler : CounterUIElement
{
    private void Start()
    {
        int waveNumber = LevelManager.Instance.CurrentLevel.WaveHandler.TotalWaves;
        //Init(waveNumber,0);
    }

    public override void Init(int maxValue, int currentValue = -1)
    {
        base.Init(maxValue, currentValue);
        LevelManager.Instance.CurrentLevel.WaveHandler.OnWaveStart += UpdateWave;
    }

    private void UpdateWave(int wave)
    {
        UpdateUIData(wave);
    }

    public override void Hide()
    {
        LevelManager.Instance.CurrentLevel.WaveHandler.OnWaveStart += UpdateWave;
        base.Hide();
    }
}