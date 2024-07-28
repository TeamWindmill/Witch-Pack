using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilitySkillTreeIconDetails : ClickableUIElement<AbilitySO, Action<AbilitySO>>
{
    [BoxGroup("Components")] [SerializeField]
    private Image bg;

    [BoxGroup("Components")] [SerializeField]
    private Image frame;

    [BoxGroup("Components")] [SerializeField]
    private Image line;

    [BoxGroup("Components")] [SerializeField]
    private Image abilitySprite;

    [Space] [BoxGroup("Sprites")] [SerializeField]
    private Sprite highlightFrameSprite;

    [BoxGroup("Sprites")] [SerializeField] private Sprite defaultFrameSprite;

    private Action<AbilitySO> _onClick;
    private AbilitySO _ability;

    public override void Init(AbilitySO data, Action<AbilitySO> onClick)
    {
        _ability = data;
        _onClick = onClick;
        bg.gameObject.SetActive(false);
        _windowInfo.Name = data.Name;
        _windowInfo.Discription = data.Discription;
        abilitySprite.sprite = data.DefaultIcon;
        base.Init(data, onClick);
        Show();
    }

    public void HighlightIcon(bool state)
    {
        if (!Initialized) return;
        frame.sprite = state ? highlightFrameSprite : defaultFrameSprite;
    }
    public void SelectIcon(bool state)
    {
        if (!Initialized) return;
        bg.gameObject.SetActive(state);
    }

    protected override void OnClick(PointerEventData eventData)
    {
        if (!Initialized) return;
        base.OnClick(eventData);
        _onClick?.Invoke(_ability);
    }
}