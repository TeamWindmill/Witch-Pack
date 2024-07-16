using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShamanUpgradeIcon : ClickableUIElement<ShamanSaveData>
{
    public ShamanSaveData ShamanSaveData { get; private set; }
    
    [SerializeField] private Image _splash;
    [SerializeField] private Image _frame;
    [SerializeField] private Sprite _defaultFrame;
    [SerializeField] private Sprite _selectedFrame;
    public override void Init(ShamanSaveData shamanSaveData)
    {
        ShamanSaveData = shamanSaveData;
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
        if(upgradeWindow != null) upgradeWindow.SelectShaman(ShamanSaveData);
        base.OnClick(eventData);
        
    }

    public void SelectIcon(bool state)
    {
        _frame.sprite = state ? _selectedFrame : _defaultFrame;
    }
}