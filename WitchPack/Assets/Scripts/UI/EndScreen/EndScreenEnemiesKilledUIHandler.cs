using TMPro;
using UnityEngine;

public class EndScreenEnemiesKilledUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _countText;


    public void UpdateUIVisual()
    {
        //_countText.text = EnemyManager.NumberOfEnemiesKilled.ToString();
    }
}