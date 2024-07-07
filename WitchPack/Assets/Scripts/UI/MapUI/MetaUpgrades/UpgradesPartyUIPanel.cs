using UnityEngine;

public class UpgradesPartyUIPanel : UIElement
{
    [SerializeField] private RectTransform _heroContainer;
    [SerializeField] private ShamanUpgradeIcon[] _shamanUpgradeIcon;

    public void Init()
    {
        Hide();
    }
    
    public override void Show()
    {
        var party = GameManager.ShamansManager.ShamanRoster;
        foreach (var shaman in party)
        {
            foreach (var icon in _shamanUpgradeIcon)
            {
                if (icon.gameObject.activeSelf) continue;
                icon.Init(shaman);
                break;
            }
        }
        base.Show();
    }

    public override void Hide()
    {
        foreach (var icon in _shamanUpgradeIcon)
        {
            icon.Hide();
        }
        base.Hide();
    }
}