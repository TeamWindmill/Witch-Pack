using TMPro;
using UnityEngine;

public class WaveCountUIHandler : BaseUIElement
{
    [SerializeField] private TMP_Text _currentCount;
    [SerializeField] private TMP_Text _maxCount;
    public override void Show()
    {
        //LevelManager.WaveManager.OnNewWaveStarted += UpdateUiData;
        base.Show();
    }

    public override void UpdateUIVisual()
    {
        base.UpdateUIVisual();
        //_maxCount.text = $"/{LevelManager.WaveManager.TotalNumberOfWaves}";
    }

    public override void Hide()
    {
        //LevelManager.WaveManager.OnNewWaveStarted -= UpdateUiData;
        base.Hide();
    }
}