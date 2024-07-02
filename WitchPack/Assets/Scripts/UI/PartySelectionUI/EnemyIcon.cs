using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIcon : MonoBehaviour
{
    [SerializeField] private Image _splash;
    [SerializeField] private TextMeshProUGUI _amountText;
    public void Init(EnemyConfig enemy, string amountText)
    {
        _splash.sprite = enemy.UnitIcon;
        _amountText.text = amountText;
    }
}
