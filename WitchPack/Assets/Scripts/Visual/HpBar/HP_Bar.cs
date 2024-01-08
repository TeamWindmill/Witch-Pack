using System.Collections;
using UnityEngine;

public class HP_Bar : MonoBehaviour
{
    [SerializeField] Transform fillSprite;
    [SerializeField] float drainDuration;
    [Header("Colors")] [SerializeField] Color defaultBarColor;
    [SerializeField] Color heroHealthColor;
    [SerializeField] Color enemyHealthColor;
    [SerializeField] Color coreHealthColor;

    float _maxValue;

    Vector3 _originalScale;
    private SpriteRenderer _fillSpriteRenderer;

    //float targetValue;
    Coroutine _runningSmoothBar;

    public void Init(float max, UnitType entityType)
    {
        //fillImage.fillAmount = 1f; //just to make sure. later on this will be removed and it will just read the value
        _originalScale = fillSprite.localScale;
        fillSprite.localScale = new Vector3(1f, _originalScale.y, _originalScale.z);

        _fillSpriteRenderer = fillSprite.GetComponentInChildren<SpriteRenderer>();
        switch (entityType)
        {
            case UnitType.Shaman:
                _fillSpriteRenderer.color = heroHealthColor;
                break;
            case UnitType.Enemy:
                _fillSpriteRenderer.color = enemyHealthColor;
                break;
            case UnitType.Temple:
                _fillSpriteRenderer.color = coreHealthColor;
                break;
            default:
                _fillSpriteRenderer.color = defaultBarColor;
                break;
        }

        _maxValue = max;
    }

    public void Init(float max)
    {
        //fillImage.fillAmount = 1f; //just to make sure. later on this will be removed and it will just read the value
        _originalScale = fillSprite.localScale;
        fillSprite.localScale = new Vector3(1f, _originalScale.y, _originalScale.z);

        _fillSpriteRenderer = fillSprite.GetComponentInChildren<SpriteRenderer>();
        _fillSpriteRenderer.color = defaultBarColor;

        _maxValue = max;
    }

    public void SetBarValue(float value)
    {
        //fillImage.fillAmount = value/ _maxValue;
        fillSprite.localScale = new Vector3(value / _maxValue, _originalScale.y, _originalScale.z);
    }

    public void SetBarValueSmoothly(float value)
    {
        //targetValue = value;
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
            SetBarValue(currentValue);
            yield return null;
        }
    }
}

public enum UnitType
{
    Shaman,
    Enemy,
    Temple
}