using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class OldSelectionHandler : MonoBehaviour,ISelection
{
    public event Action<Shaman> OnShamanSelect;
    public event Action<Shaman> OnShamanDeselected;
    public event Action<Shadow> OnShadowSelect;
    public event Action<Shadow> OnShadowDeselected;
    public SelectionType SelectMode { get; private set; }

    public Shaman SelectedShaman { get; private set; }

    public bool ShadowSelected { get; private set; }
    public Shadow Shadow => shadow;

    [SerializeField] private Shadow shadow;
    private const int LEFT_CLICK = 0;
    private const int RIGHT_CLICK = 1;
    private const int MIDDLE_CLICK = 2;
    private bool _mouseOverSelectionUI => HeroSelectionUI.Instance.isMouseOver;
    [SerializeField] private float _maxHoldTime;
    private float _currentHoldTime;
    private bool _inSelectMode;

    private void Start()
    {
        shadow.Hide();
        SelectedShaman = LevelManager.Instance.ShamanParty[0];
        HeroSelectionUI.Instance.Show(SelectedShaman);
        SelectMode = SelectionType.Info;
    }
    public void OnShamanClick(PointerEventData.InputButton button, Shaman shaman)
    {
        if (button == PointerEventData.InputButton.Right)
        {
            if(SelectedShaman != null) SelectedShaman.ShamanVisualHandler.HideShamanRing();
            SelectedShaman = shaman;
            SelectMode = SelectionType.Info;
            HeroSelectionUI.Instance.Show(shaman);
            shaman.ShamanVisualHandler.ShowShamanRing();
        }
        if (button == PointerEventData.InputButton.Left)
        {
            CancelMove();
            if(SelectedShaman != null) SelectedShaman.ShamanVisualHandler.HideShamanRing();
            SelectedShaman = shaman;
            SelectMode = SelectionType.Movement;
            HeroSelectionUI.Instance.Show(shaman);
            shaman.ShamanVisualHandler.ShowShamanRing();
            Shadow.Show(shaman);
            SelectMove();
        }
        
    }

    private void Update()
    {
        if (ReferenceEquals(SelectedShaman, null)) return;
        if (UIManager.MouseOverUI) return;
        if (SelectMode == SelectionType.Info)
        {
            if (!_mouseOverSelectionUI)
            {
                if (_currentHoldTime < _maxHoldTime)
                {
                    if (Input.GetMouseButtonUp(LEFT_CLICK))
                    {
                        QuickMove();
                        _currentHoldTime = 0;
                        return;
                    }
                }
                if (Input.GetMouseButton(LEFT_CLICK))
                {
                    foreach (var shaman in LevelManager.Instance.ShamanParty)
                    {
                        if (shaman.MouseOverShaman && shaman != SelectedShaman)
                        {
                            return;
                        }
                    }
                    if (_currentHoldTime > _maxHoldTime && !_inSelectMode) //holding
                    {
                        _inSelectMode = true;
                        SelectMove();
                    }
                    else
                    {
                        _currentHoldTime += Time.deltaTime;
                    }
                    if (Input.GetMouseButtonDown(RIGHT_CLICK))
                    {
                        CancelMove();
                    }
                }
                if (Input.GetMouseButtonUp(LEFT_CLICK))
                {
                    _currentHoldTime = 0;
                    _inSelectMode = false;
                    if (_mouseOverSelectionUI) return;
                    ReleaseMove();
                }
                //if (Input.GetMouseButtonDown(LEFT_CLICK)) CloseUIPanelAndDeselectShaman();
            }
        }

        if (SelectMode == SelectionType.Movement)
        {
            if (Input.GetMouseButtonUp(LEFT_CLICK))
            { 
                if (_mouseOverSelectionUI) return;
                foreach (var shaman in LevelManager.Instance.ShamanParty)
                {
                    if (shaman.MouseOverShaman)
                    {
                        return;
                    }
                }

                ReleaseMove();
                SelectMode = SelectionType.Info;
            }
            if (Input.GetMouseButtonDown(RIGHT_CLICK))
            { 
                CancelMove();
                SelectMode = SelectionType.Info;
            }
        }
    }

    private void SelectMove()
    {
        SlowMotionManager.Instance.StartSlowMotionEffects();
        shadow.Show(SelectedShaman);
        ShadowSelected = true;
        OnShamanSelect?.Invoke(SelectedShaman);
    }
    private void ReleaseMove()
    {
        SelectMode = SelectionType.Info;
        if (!shadow.IsActive) return;

        SlowMotionManager.Instance.EndSlowMotionEffects();
        shadow.Hide();
        ShadowSelected = false;
        var newDest = GameManager.CameraHandler.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        SelectedShaman.Movement.SetDestination(newDest);
        OnShamanDeselected?.Invoke(SelectedShaman);
    }
    private void QuickMove()
    {
        var newDest = GameManager.CameraHandler.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        SelectedShaman.Movement.SetDestination(newDest);
        //OnShamanMoveSelect?.Invoke(_selectedShaman);
    }
    private void CancelMove()
    {
        SlowMotionManager.Instance.EndSlowMotionEffects();
        shadow.Hide();
        ShadowSelected = false;
        OnShamanDeselected?.Invoke(SelectedShaman);
        SelectedShaman = null;
    }

    private void CloseUIPanelAndDeselectShaman()
    {
        HeroSelectionUI.Instance.Hide();
        SelectedShaman = null;
    }

    public bool GetOnMouseDownShaman()
    {
        return false;
    }
}
