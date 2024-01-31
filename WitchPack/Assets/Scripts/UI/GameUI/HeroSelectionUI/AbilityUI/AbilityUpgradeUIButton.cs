using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityUpgradeUIButton : ClickableUIElement
{
    public event Action<AbilityUpgradeUIButton> OnAbilityClick;
    public BaseAbility Ability => _ability;

    [SerializeField] private Image bg;
    [SerializeField] private Image lockedBg;
    [SerializeField] private Image frame;
    [SerializeField] private Image abilitySprite;
    [Space] [SerializeField] private Sprite upgradeReadyFrameSprite;
    [SerializeField] private Sprite defaultFrameSprite;

    private BaseAbility _ability;


    public void Init(BaseAbility ability)
    {
        abilitySprite.sprite = ability.Icon;
        _ability = ability;
        Show();
    }

    public override void Show()
    {
        switch (_ability.AbilityUpgradeState)
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

    protected override void OnClick(PointerEventData eventData)
    {
        base.OnClick(eventData);
        switch (_ability.AbilityUpgradeState)
        {
            case AbilityUpgradeState.Locked:
                return;
            case AbilityUpgradeState.Open:
                _ability.UpgradeAbility();
                OnAbilityClick?.Invoke(this);
                return;
            case AbilityUpgradeState.Upgraded:
                return;
        }
    }
}

public enum AbilityUpgradeState
{
    Locked,
    Open,
    Upgraded
}