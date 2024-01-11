using UnityEngine;

public abstract class UIElement : MonoBehaviour
{
    //inherit from this class if it is a ui element
    [SerializeField, HideInInspector] protected RectTransform rectTransform;
    [SerializeField] private bool _showOnAwake = true;

    public RectTransform RectTransform => rectTransform;

    protected virtual void Awake()
    {
        if (_showOnAwake)
            Show();
        else
            gameObject.SetActive(false);
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void UpdateVisual()
    {
        
    }
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
    private void OnValidate()
    {
        rectTransform ??= GetComponent<RectTransform>();
    }
}
