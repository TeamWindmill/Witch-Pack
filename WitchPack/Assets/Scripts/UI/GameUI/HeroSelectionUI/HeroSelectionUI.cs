using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelectionUI : MonoSingleton<HeroSelectionUI>
{
    [SerializeField] private Image _shamanSprite;
    [SerializeField] private TextMeshProUGUI _shamanName;
    [SerializeField] private StatBlockPanel _statBlockPanel;
    [SerializeField] private PSBonusUIHandler _psBonusUIHandler;
    [SerializeField] private AbilityUIHandler _abilityUIHandler;

    public bool IsActive { get; private set; }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(Shaman shaman)
    {
        UnitStats stats = shaman.Stats;
        
        _statBlockPanel.Init(shaman);
        _psBonusUIHandler.Show(stats);
        _abilityUIHandler.Show(shaman.CastingHandlers);
        _shamanSprite.sprite = shaman.ShamanConfig.UnitIcon;
        _shamanName.text = shaman.ShamanConfig.Name;
        
        IsActive = true;
        gameObject.SetActive(true);
    }

    public void UpdateStatBlocks(StatType shamanStatType, float newValue)
    {
        _statBlockPanel.UpdateStatBlocks(shamanStatType, newValue);
    }

    public void Hide()
    {
        _statBlockPanel.HideStatBlocks();
        _psBonusUIHandler.Hide();
        _abilityUIHandler.Hide();

        IsActive = false;
        gameObject.SetActive(false);
    }
}