using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroSelectionUI : MonoSingleton<HeroSelectionUI> , IPointerEnterHandler , IPointerExitHandler
{
    public event Action OnMouseEnter;
    public event Action OnMouseExit;
    [SerializeField] private Image shamanSprite;
    [SerializeField] private TextMeshProUGUI shamanName;
    [SerializeField] private TextMeshProUGUI shamanLevel;
    [SerializeField] private StatBlockPanel statBlockPanel;
    [SerializeField] private PSBonusUIHandler psBonusUIHandler;
    [SerializeField] private AbilitiesHandlerUI abilitiesHandlerUI;

    public bool IsActive { get; private set; }
    public bool MouseOverUI { get; private set; }
    public Shaman Shaman { get; private set; }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(Shaman shaman)
    {
        UnitStats stats = shaman.Stats;
        Shaman = shaman;
        statBlockPanel.Init(shaman);
        //psBonusUIHandler.Show(stats);
        abilitiesHandlerUI.Show(shaman);
        shamanSprite.sprite = shaman.ShamanConfig.UnitIcon;
        shamanName.text = shaman.ShamanConfig.Name;
        shamanLevel.text = "Lvl: " + shaman.EnergyHandler.ShamanLevel;
        shaman.EnergyHandler.OnShamanLevelUp += OnShamanLevelUp;
        
        IsActive = true;
        gameObject.SetActive(true);
    }

    private void OnShamanLevelUp(int level)
    {
        shamanLevel.text = "Lvl: " + level;
    }

    public void UpdateStatBlocks(StatType shamanStatType, float newValue) => statBlockPanel.UpdateStatBlocks(shamanStatType, newValue);

    public void Hide()
    {
        statBlockPanel.HideStatBlocks();
        psBonusUIHandler.Hide();
        abilitiesHandlerUI.Hide();
        IsActive = false;
        gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseOverUI = true;
        OnMouseEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseOverUI = false;
        OnMouseExit?.Invoke();
    }
}