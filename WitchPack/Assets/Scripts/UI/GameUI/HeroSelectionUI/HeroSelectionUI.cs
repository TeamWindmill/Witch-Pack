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
    [SerializeField] private StatBlockPanel statBlockPanel;
    [SerializeField] private PSBonusUIHandler psBonusUIHandler;
    [SerializeField] private AbilitiesHandlerUI abilitiesHandlerUI;

    public bool IsActive { get; private set; }
    public bool MouseOverUI { get; private set; }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(Shaman shaman)
    {
        UnitStats stats = shaman.Stats;
        
        statBlockPanel.Init(shaman);
        psBonusUIHandler.Show(stats);
        abilitiesHandlerUI.Show(shaman.CastingHandlers);
        shamanSprite.sprite = shaman.ShamanConfig.UnitIcon;
        shamanName.text = shaman.ShamanConfig.Name;
        
        IsActive = true;
        gameObject.SetActive(true);
    }

    public void UpdateStatBlocks(StatType shamanStatType, int newValue) => statBlockPanel.UpdateStatBlocks(shamanStatType, newValue);

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