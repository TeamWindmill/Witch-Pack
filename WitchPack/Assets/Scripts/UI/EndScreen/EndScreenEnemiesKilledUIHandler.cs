using TMPro;
using UnityEngine;

public class EndScreenEnemiesKilledUIHandler : UIElement
{
    [SerializeField] private TMP_Text _countText;

    public override void Show()
    {
        base.Show();
        _countText.text = "dont know how many enemies you killed...";
    }
}