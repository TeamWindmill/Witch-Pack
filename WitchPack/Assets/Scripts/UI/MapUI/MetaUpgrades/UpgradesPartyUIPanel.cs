using UnityEngine;

public class UpgradesPartyUIPanel : UIElement
{
    [SerializeField] private RectTransform _heroContainer;
    [SerializeField] private ShamanUpgradeIcon[] _shamanUpgradeIcons;

    public void Init()
    {
        Hide();
    }
    
    public override void Show()
    {
        var party = GameManager.ShamansManager.ShamanRoster;
        foreach (var shaman in party)
        {
            foreach (var icon in _shamanUpgradeIcons)
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
        foreach (var icon in _shamanUpgradeIcons)
        {
            icon.Hide();
        }
        base.Hide();
    }

    public void SelectShamanIcon(ShamanSaveData shaman)
    {
        foreach (var icon in _shamanUpgradeIcons)
        {
            if (shaman == icon.ShamanSaveData)
            {
                icon.SelectIcon(true);
                continue;
            }
            icon.SelectIcon(false);
        }
        
    }
}