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
    private bool shouldIndicatorPulse;
    private Action onClickAction;

    public Action OnVisible;
    public Action OnInvisible;

    public Renderer Rend { get => rend; }
    public Sprite ArtWork { get => artWork; }
    public float Lifetime { get => lifetime;}
    public bool Clickable { get => clickable;}
    public Action OnClickAction { get => onClickAction;}
    public Indicator CurrentIndicator { get => currentIndicator; }
    public bool ShouldIndicatorPulse { get => shouldIndicatorPulse; }

    public void Init(Sprite art, Action action = null, float lifetime = 0, bool clickable = false, bool shouldIndicatorPulse = false)
    {
        this.lifetime = lifetime;
        artWork = art;
        onClickAction = action;
        this.clickable = clickable;
        this.shouldIndicatorPulse = shouldIndicatorPulse;
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
        if (!Application.isPlaying || !toggleOnVis)
        {
            return;
        }

        OnInvisible?.Invoke();
        SetCurrentIndicator();
    }



    [ContextMenu("Test Indicator")]
    public void SetCurrentIndicator()
    {
        currentIndicator = LevelManager.Instance?.IndicatorManager.CreateIndicator(this);
    }

    public void ToggleIndicatableRendering(bool state)
    {
        toggleOnVis = state;
        if (!ReferenceEquals(currentIndicator, null))
        {
            currentIndicator.gameObject.SetActive(state);
        }

        if(state == false)
        {
            currentIndicator = null;
        }
    }


}
