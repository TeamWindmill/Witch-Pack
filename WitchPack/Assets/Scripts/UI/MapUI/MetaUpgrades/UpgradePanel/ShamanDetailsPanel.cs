using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShamanDetailsPanel : UIElement<ShamanSaveData>
{
    [SerializeField] private Image _shamanSplash;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _skillPointsText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private StatBar _expBar;
    [SerializeField] private StatBlockUI[] _statBlocks;

    private ShamanSaveData _shaman;
    public override void Init(ShamanSaveData data)
    {
        _shaman = data;
        _nameText.text = data.Config.Name;
        _shamanSplash.sprite = data.Config.UnitSprite;
        _descriptionText.text = data.Config.Description;
        _skillPointsText.text = "SP " + data.ShamanExperienceHandler.AvailableSkillPoints;
        _expBar.Init(new StatBarData($"LVL {data.ShamanExperienceHandler.ShamanLevel}", data.ShamanExperienceHandler.CurrentExp, data.ShamanExperienceHandler.MaxExpToNextLevel));
        data.ShamanExperienceHandler.OnShamanGainExp += _expBar.UpdateStatbar;
        for (int i = 0; i < _statBlocks.Length; i++)
        {
            //_statBlocks[i].Init(data.Stats[i]);
        }
        base.Init(data);
    }

    public override void Refresh()
    {
        _skillPointsText.text = _shaman.ShamanExperienceHandler.AvailableSkillPoints.ToString();
        _levelText.text = "LVL " + _shaman.ShamanExperienceHandler.ShamanLevel;
    }

    public override void Hide()
    {
        if(_shaman != null) _shaman.ShamanExperienceHandler.OnShamanGainExp -= _expBar.UpdateStatbar;
        _expBar.Hide();
        base.Hide();
    }
}