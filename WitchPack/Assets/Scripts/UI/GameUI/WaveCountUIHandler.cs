using System;
using TMPro;
using UnityEngine;

public class WaveCountUIHandler : CounterUIElement
{
    public override void Init()
    {
        base.Init();
        ElementInit(LevelManager.Instance.CurrentLevel.WaveHandler.TotalWaves,LevelManager.Instance.CurrentLevel.WaveHandler.CurrentWave);
    }
    
    public override void ElementInit(int maxValue, int currentValue = -1)
    {
        base.ElementInit(maxValue, currentValue);
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