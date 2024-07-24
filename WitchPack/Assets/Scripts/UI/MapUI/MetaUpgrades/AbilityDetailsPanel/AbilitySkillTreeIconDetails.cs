using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySkillTreeIconDetails : ClickableUIElement<AbilitySO>
{
    [BoxGroup("Components")][SerializeField] private Image bg;
    [BoxGroup("Components")][SerializeField] private Image frame;
    [BoxGroup("Components")][SerializeField] private Image line;
    [BoxGroup("Components")][SerializeField] private Image abilitySprite;
    [Space] 
    [BoxGroup("Sprites")][SerializeField] private Sprite highlightFrameSprite;
    [BoxGroup("Sprites")][SerializeField] private Sprite defaultFrameSprite;
    public override void Init(AbilitySO data)
    {
        _windowInfo.Name = data.Name;
        _windowInfo.Discription = data.Discription;
        abilitySprite.sprite = data.DefaultIcon;
        base.Init(data);
        Show();
    }

    public void HighlightIcon(bool state)
    {
        frame.sprite = state ? highlightFrameSprite : defaultFrameSprite;
    }
}
