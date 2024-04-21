using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroSelectionUI : UIElement 
{
    public static HeroSelectionUI Instance { get; private set; }
    public event Action OnMouseEnter;
    public event Action OnMouseExit;
    public StatBlockPanel StatBlockPanel => statBlockPanel;
    public AbilitiesHandlerUI AbilitiesHandlerUI => abilitiesHandlerUI;
    public bool IsActive { get; private set; }
    public Shaman Shaman { get; private set; }

    [BoxGroup("Hero Selection")][SerializeField] private Image shamanSprite;
    [BoxGroup("Hero Selection")][SerializeField] private TextMeshProUGUI shamanName;
    [BoxGroup("Hero Selection")][SerializeField] private TextMeshProUGUI shamanLevel;
    [BoxGroup("Hero Selection")][SerializeField] private StatBlockPanel statBlockPanel;
    [BoxGroup("Hero Selection")][SerializeField] private AbilitiesHandlerUI abilitiesHandlerUI;

    protected override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else Instance = this;
        base.Awake();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(Shaman shaman)
    {
        Shaman = shaman;
        statBlockPanel.Init(shaman);
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

    public void Hide()
    {
        statBlockPanel.HideStatBlocks();
        abilitiesHandlerUI.Hide();
        IsActive = false;
        gameObject.SetActive(false);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.Instance.CameraHandler.ToggleCameraLock(true);
        OnMouseEnter?.Invoke();
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Instance.CameraHandler.ToggleCameraLock(false);
        OnMouseExit?.Invoke();
        base.OnPointerExit(eventData);
    }
}