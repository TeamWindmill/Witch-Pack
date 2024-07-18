using TMPro;
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
    [SerializeField] private TextMeshProUGUI _skillPointText;
    [SerializeField] private Image _skillPointFrame;
    [SerializeField] private Sprite _defaultSkillPointFrame;
    [SerializeField] private Sprite _HighlightSkillPointFrame;
    public override void Init(ShamanSaveData shamanSaveData)
    {
        ShamanSaveData = shamanSaveData;
        _splash.sprite = shamanSaveData.Config.UnitIcon;
        _skillPointText.text = shamanSaveData.ShamanExperienceHandler.AvailableSkillPoints.ToString();
        HighlightSkillPointsFrame(ShamanUpgradePanel.CheckIfUpgradeAvailable(shamanSaveData));
        base.Init(shamanSaveData);
        Show();
    }

    public override void Show()
    {
        if(!Initialized) return;
        base.Show();
    }
    public override void Refresh()
    {
        if(!Initialized) return;
        _skillPointText.text = ShamanSaveData.ShamanExperienceHandler.AvailableSkillPoints.ToString();
        HighlightSkillPointsFrame(ShamanUpgradePanel.CheckIfUpgradeAvailable(ShamanSaveData));
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
    public void HighlightSkillPointsFrame(bool state)
    {
        _skillPointFrame.sprite = state ? _HighlightSkillPointFrame : _defaultSkillPointFrame;
    }
}