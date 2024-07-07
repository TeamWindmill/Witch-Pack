using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShamanUpgradeIcon : ClickableUIElement<ShamanSaveData>
{
    [SerializeField] private Image _splash;
    //[Space] 
    //[SerializeField] private Sprite _deadUnitIcon;
    private ShamanSaveData _shamanSaveData;
    public override void Init(ShamanSaveData shamanSaveData)
    {
        _shamanSaveData = shamanSaveData;
        _splash.sprite = shamanSaveData.Config.UnitIcon;
        base.Init(shamanSaveData);
        Show();
    }

    public override void Show()
    {
        if(!Initialized) return;
        base.Show();
    }

    protected override void OnClick(PointerEventData eventData)
    {
        if(!Initialized) return;
        var upgradeWindow = WindowManager as UpgradeWindow;
        if(upgradeWindow != null) upgradeWindow.SelectShaman(_shamanSaveData);
        base.OnClick(eventData);
        
    }
}