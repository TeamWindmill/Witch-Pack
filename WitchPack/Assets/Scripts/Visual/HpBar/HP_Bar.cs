using System;
using System.Collections;
using UnityEngine;

public class HP_Bar : MonoBehaviour
{
    [SerializeField] Transform fillSprite;
    [SerializeField] SpriteRenderer fillSpriteRenderer;
    [SerializeField] float drainDuration;
    [Header("Colors")] [SerializeField] Color defaultBarColor;
    [SerializeField] Color heroHealthColor;
    [SerializeField] Color enemyHealthColor;
    [SerializeField] Color coreHealthColor;


    Vector3 _originalScale;
    float _maxValue;

    Coroutine _runningSmoothBar;

    private void OnValidate()
    {
        fillSpriteRenderer ??= fillSprite.GetComponentInChildren<SpriteRenderer>();
    }

    public void Init(float max, UnitType entityType)
    {
        _originalScale = fillSprite.localScale;
        fillSprite.localScale = new Vector3(1f, _originalScale.y, _originalScale.z);

        switch (entityType)
        {
            case UnitType.Shaman:
                fillSpriteRenderer.color = heroHealthColor;
                break;
            case UnitType.Enemy:
                fillSpriteRenderer.color = enemyHealthColor;
                break;
            case UnitType.Temple:
                fillSpriteRenderer.color = coreHealthColor;
                break;
            default:
                fillSpriteRenderer.color = defaultBarColor;
                break;
        }

        _maxValue = max;
    }

    public void Init(float max)
    {
        _originalScale = fillSprite.localScale;
        fillSprite.localScale = new Vector3(1f, _originalScale.y, _originalScale.z);

        fillSpriteRenderer = fillSprite.GetComponentInChildren<SpriteRenderer>();
        fillSpriteRenderer.color = defaultBarColor;

        _maxValue = max;
    }

    public void SetBarValue(int value)
    {
        fillSprite.localScale = new Vector3(value / _maxValue, _originalScale.y, _originalScale.z);
    }

    public void SetBarValueSmoothly(float value)
    {
        if (_runningSmoothBar != null)
        {
            StopCoroutine(_runningSmoothBar);
        }

        _runningSmoothBar = StartCoroutine(SmoothBar(drainDuration, value / _maxValue));
    }

    IEnumerator SmoothBar(float duration, float targetValue)
    {
        float t = 0f;
        float startValue = fillSprite.localScale.x;
        float currentValue = fillSprite.localScale.x;
        while (t <= duration)
        {
            currentValue = Mathf.Lerp(startValue, targetValue, t);
            t += Time.deltaTime / duration;
            SetBarValue((int)currentValue);
            yield return null;
        }
    }

    public void SetBarValue(Damageable arg1, DamageDealer arg2, DamageHandler arg3, BaseAbility arg4, bool isCrit)
    {
        var ratio = arg1.CurrentHp / _maxValue;
        if (ratio < 0) ratio = 0;
        fillSprite.localScale = new Vector3(ratio, _originalScale.y, _originalScale.z);
    }
}

public enum UnitType
{
    Shaman,
    Enemy,
    Temple
}