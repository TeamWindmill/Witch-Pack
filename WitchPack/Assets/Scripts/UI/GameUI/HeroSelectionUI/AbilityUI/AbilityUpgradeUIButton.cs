using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityUpgradeUIButton : ClickableUIElement
{
    public event Action<AbilityUpgradeUIButton> OnAbilityClick;
    public AbilityUpgradeState AbilityUpgradeState => _abilityUpgradeState;
    public BaseAbility BaseAbility => _baseAbility;

    [SerializeField] private Image bg;
    [SerializeField] private Image lockedBg;
    [SerializeField] private Image frame;
    [SerializeField] private Image abilitySprite;
    [Space] [SerializeField] private Sprite upgradeReadyFrameSprite;
    [SerializeField] private Sprite defaultFrameSprite;

    private AbilityUpgradeState _abilityUpgradeState;
    private BaseAbility _baseAbility;


    public void Init(BaseAbility ability)
    {
        abilitySprite.sprite = ability.Icon;
        _baseAbility = ability;
        Show();
    }

    public override void Show()
    {
        switch (_abilityUpgradeState)
        {
            case AbilityUpgradeState.Locked:
                lockedBg.gameObject.SetActive(true);
                frame.sprite = defaultFrameSprite;
                break;
            case AbilityUpgradeState.Open:
                lockedBg.gameObject.SetActive(false);
                frame.sprite = upgradeReadyFrameSprite;
                break;
            case AbilityUpgradeState.Upgraded:
                lockedBg.gameObject.SetActive(false);
                frame.sprite = defaultFrameSprite;
                break;
        }

        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }

    public void ChangeState(AbilityUpgradeState state)
    {
        _abilityUpgradeState = state;
        Show();
    }

    protected override void OnClick(PointerEventData eventData)
    {
        base.OnClick(eventData);
        switch (_abilityUpgradeState)
        {
            case AbilityUpgradeState.Locked:
                return;
            case AbilityUpgradeState.Open:
                OnAbilityClick?.Invoke(this);
                break;
            case AbilityUpgradeState.Upgraded:
                return;
        }

        base.OnClick(eventData);
    }
}

public enum AbilityUpgradeState
{
    Locked,
    Open,
    Upgraded
}