using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private Image _abilitySpriteRenderer;
    [SerializeField] private Image _cooldownSpriteRenderer;

    private UnitCastingHandler _abilityCaster;
    public bool IsActive => _isActive;
    private bool _isActive;

    private void Start()
    {
        _abilitySpriteRenderer.enabled = false;
        _cooldownSpriteRenderer.enabled = false;
    }

    public void Show(UnitCastingHandler abilityCaster)
    {
        _abilityCaster = abilityCaster;
        _abilitySpriteRenderer.enabled = true;
        _cooldownSpriteRenderer.enabled = true;
        _abilitySpriteRenderer.sprite = abilityCaster.Ability.Icon;
        _isActive = true;
    }
    private void Update()
    {
        if (!_isActive) return;
        UpdateCooldownFillAmount(_abilityCaster);
    }

    private void UpdateCooldownFillAmount(UnitCastingHandler abilityCaster)
    {
        float ratio = 0;
        if (abilityCaster.LastCast > 0)
        {
            ratio = (GAME_TIME.GameTime - abilityCaster.LastCast) / abilityCaster.Ability.Cd;
            if (ratio < 0) ratio = 0;
        }
        _cooldownSpriteRenderer.fillAmount = ratio;
    }
    public void Hide()
    {
        _abilitySpriteRenderer.enabled = false;
        _cooldownSpriteRenderer.enabled = false;
        _isActive = false;
    }
}