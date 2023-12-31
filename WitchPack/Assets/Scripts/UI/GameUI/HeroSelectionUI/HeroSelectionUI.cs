using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelectionUI : MonoSingleton<HeroSelectionUI>
{
    public bool IsInitialization { get; }

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

    // public void Init(UnitEntity shaman)
    // {
    //     IEnumerable<Stat> stats = shaman.EntityStatComponent.GetAllStats();
    //     
    //     _statBlockPanel.Init(stats);
    //     _psBonusUIHandler.Show(stats);
    //     _abilityUIHandler.Show(shaman.EntityAbilitiesComponent.Abilities);
    //     _shamanSprite.sprite = shaman.Config.VisualComponentConfig.Icon;
    //     _shamanName.text = shaman.Config.EntityName;
    //     
    //     IsActive = true;
    //     gameObject.SetActive(true);
    // }

    // public void UpdateStatBlocks(Stat shamanStat, float newValue)
    // {
    //     _statBlockPanel.UpdateStatBlocks(shamanStat, newValue);
    // }

    public void HideSelectionUI()
    {
        //_statBlockPanel.HideStatBlocks();
        _psBonusUIHandler.Hide();
        _abilityUIHandler.Hide();

        IsActive = false;
        gameObject.SetActive(false);
    }
}