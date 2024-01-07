using TMPro;
using UnityEngine;


public class EndScreenWaveInteractiveUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;


    public void UpdateUIVisual()
    {
        //_text.text = $"{LevelManager.WaveManager.WaveNumber}/{LevelManager.WaveManager.TotalNumberOfWaves}";
    }
}