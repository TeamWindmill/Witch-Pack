using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityUpgradeUI : ClickableUIElement
{
    public event Action<BaseAbility> OnAbilityClick;
    
    [SerializeField] private Image bg;
    [SerializeField] private Image lockedBg;
    [SerializeField] private Image frame;
    [SerializeField] private Image abilitySprite;
    [Space] 
    [SerializeField] private Sprite upgradeReadyFrameSprite;
    [SerializeField] private Sprite defaultFrameSprite;

    private BaseAbility _baseAbility;


    public void Init(BaseAbility ability)
    {
        abilitySprite.sprite = ability.Icon;
        Show();
    }

  /*  public override void Show()
    {
        switch (_baseAbility.AbilityUpgradeState)
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

    // public void ChangeState(AbilityUpgradeState state)
    // {
    //     //_abilityState = state;
    //     Show();
    // }

    protected override void OnClick(PointerEventData eventData)
    {
        switch (_baseAbility.AbilityUpgradeState)
        {
            case AbilityUpgradeState.Locked:
                return;
            case AbilityUpgradeState.Open:
                //upgrade ability
                break;
            case AbilityUpgradeState.Upgraded:
                return;
        }
        base.OnClick(eventData);
    }*/
}

public enum AbilityUpgradeState
{
    Locked,
    Open,
    Upgraded
}
