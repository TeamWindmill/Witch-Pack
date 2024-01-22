using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityUpgradeUI : ClickableUIElement
{
    [SerializeField] private Image bg;
    [SerializeField] private Image frame;
    [SerializeField] private Image ability;
    [Space] 
    [SerializeField] private Sprite lockedBgSprite;
    [SerializeField] private Sprite defaultBgSprite;
    [SerializeField] private Sprite upgradeReadyFrameSprite;
    [SerializeField] private Sprite defaultFrameSprite;

    private bool _isLocked;

    protected override void OnClick(PointerEventData eventData)
    {
        if (_isLocked) return;
        base.OnClick(eventData);
    }
}
