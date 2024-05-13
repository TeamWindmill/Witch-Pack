using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityUpgradeUIButton : ClickableUIElement
{
    public event Action<AbilityUpgradeUIButton> OnAbilityClick;
    public Ability Ability{ get; private set; }

    [BoxGroup("Components")][SerializeField] private Image bg;
    [BoxGroup("Components")][SerializeField] private Image frame;
    [BoxGroup("Components")][SerializeField] private Image line;
    [BoxGroup("Components")][SerializeField] private Image abilitySprite;
    [Space] 
    [BoxGroup("Sprites")][SerializeField] private Sprite upgradeReadyFrameSprite;
    [BoxGroup("Sprites")][SerializeField] private Sprite defaultFrameSprite;
    [BoxGroup("Sprites")][SerializeField] private Sprite defaultLineSprite;
    [BoxGroup("Sprites")][SerializeField] private Sprite upgradedLineSprite;
    [SerializeField] private bool showLine;

    private bool _hasSkillPoints;


    public void Init(Ability ability, bool hasSkillPoints)
    {
        Ability = ability;
        _hasSkillPoints = hasSkillPoints;
        _windowInfo.Name = ability.BaseConfig.Name;
        _windowInfo.Discription = ability.BaseConfig.Discription;
        line.gameObject.SetActive(showLine);
        Show();
    }

    public override void Show()
    {
        switch (Ability.AbilityUpgradeState)
        {
            case AbilityUpgradeState.Locked:
                if(showLine) line.sprite = defaultLineSprite;
                frame.sprite = defaultFrameSprite;
                abilitySprite.sprite = Ability.BaseConfig.DisabledIcon;
                break;
            case AbilityUpgradeState.Open:
                if (!_hasSkillPoints)
                {
                    if(showLine) line.sprite = defaultLineSprite;
                    frame.sprite = defaultFrameSprite;
                    abilitySprite.sprite = Ability.BaseConfig.DisabledIcon;
                }
                else
                {
                    if(showLine) line.sprite = defaultLineSprite;
                    frame.sprite = upgradeReadyFrameSprite;
                    abilitySprite.sprite = Ability.BaseConfig.UpgradeIcon;
                }
                break;
            case AbilityUpgradeState.Upgraded:
                if(showLine) line.sprite = upgradedLineSprite;
                frame.sprite = defaultFrameSprite;
                abilitySprite.sprite = Ability.BaseConfig.DefaultIcon;
                break;
        }

        base.Show();
    }

    public override void Hide()
    {
        if (OnAbilityClick is not null)
        {
            OnAbilityClick = null;
            //Debug.Log($"{_ability.name} Hidden");
        }
        base.Hide();
    }

    protected override void OnClick(PointerEventData eventData)
    {
        base.OnClick(eventData);
        switch (Ability.AbilityUpgradeState)
        {
            case AbilityUpgradeState.Locked:
                SoundManager.Instance.PlayAudioClip(SoundEffectType.MenuClick);
                return;
            case AbilityUpgradeState.Open:
                if(!_hasSkillPoints) return;
                Ability.UpgradeAbility();
                OnAbilityClick?.Invoke(this);
                SoundManager.Instance.PlayAudioClip(SoundEffectType.UpgradeAbility);
                return;
            case AbilityUpgradeState.Upgraded:
                SoundManager.Instance.PlayAudioClip(SoundEffectType.MenuClick);
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