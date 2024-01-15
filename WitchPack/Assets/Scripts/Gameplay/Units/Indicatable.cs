using System;
using UnityEngine;

public class Indicatable : MonoBehaviour
{
    [SerializeField] private Renderer rend;
    [SerializeField] private bool toggleOnVis;
    [SerializeField] private bool clickable;
    private Indicator currentIndicator;
    private Sprite artWork;
    private float lifetime;
    private Action onClickAction;

    public Action OnVisible;
    public Action OnInvisible;

    public Renderer Rend { get => rend; }

    public void Init(Sprite art, Action action = null, float lifetime = 0)
    {
        this.lifetime = lifetime;
        artWork = art;
        onClickAction = action;
    }


    private void OnBecameVisible()
    {
        if (!toggleOnVis)
        {
            return;
        }

        OnVisible?.Invoke();

        if (!ReferenceEquals(currentIndicator, null))
        {
            currentIndicator.gameObject.SetActive(false);
        }

        currentIndicator = null;
    }

    private void OnBecameInvisible()
    {
        if (!toggleOnVis)
        {
            return;
        }

        OnInvisible?.Invoke();
        SetCurrentIndicator();
    }



    [ContextMenu("Test Indicator")]
    public void SetCurrentIndicator()
    {
        currentIndicator = LevelManager.Instance.IndicatorManager.CreateIndicator(this, artWork, lifetime, clickable, onClickAction);
    }


}
