using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionHandler3 : MonoBehaviour, ISelection
{
    public event Action<Shaman> OnShamanSelect;
    public event Action<Shaman> OnShamanDeselected;
    public event Action<Shadow> OnShadowSelect;
    public event Action<Shadow> OnShadowDeselected;
    public SelectionType SelectMode => _selectMode;
    public Shaman SelectedShaman => _selectedShaman;
    public bool ShadowSelected { get; }
    public Shadow Shadow => shadow;

    [SerializeField] private Shadow shadow;
    private const int LEFT_CLICK = 0;
    private const int RIGHT_CLICK = 1;
    private const int MIDDLE_CLICK = 2;
    private bool _mouseOverSelectionUI => HeroSelectionUI.Instance.isMouseOver;
    private Shaman _selectedShaman;
    private SelectionType _selectMode;
    [SerializeField] private float _maxHoldTime;
    private float _currentHoldTime;
    private bool _inSelectMode;

    private void Start()
    {
        shadow.Hide();
        _selectedShaman = LevelManager.Instance.ShamanParty[0];
        HeroSelectionUI.Instance.Show(_selectedShaman);
    }
    public void OnShamanClick(PointerEventData.InputButton button, Shaman shaman)
    {
        if (button == PointerEventData.InputButton.Right)
        {
            _selectedShaman = shaman;
            _selectMode = SelectionType.Info;
            HeroSelectionUI.Instance.Show(shaman);
        }
        if (button == PointerEventData.InputButton.Left)
        {
            _selectedShaman = shaman;
            _selectMode = SelectionType.Movement;
            HeroSelectionUI.Instance.Show(shaman);
        }
    }

    private void Update()
    {
        if (ReferenceEquals(_selectedShaman, null)) return;

        if (SelectMode == SelectionType.Movement)
        {
            if (Input.GetMouseButton(LEFT_CLICK))
            {
                if (_currentHoldTime > _maxHoldTime && !_inSelectMode)
                {
                    _inSelectMode = true;
                    SelectMove();
                }
                else
                {
                    _currentHoldTime += Time.deltaTime;
                }
            }
            if (Input.GetMouseButtonUp(LEFT_CLICK))
            {
                _currentHoldTime = 0;
                _inSelectMode = false;
                if (_mouseOverSelectionUI) return;
                ReleaseMove();
            }
            if (Input.GetMouseButtonDown(RIGHT_CLICK))
            {
                CancelMove();
            }
        }
        if (SelectMode == SelectionType.Info)
        {
            if (Input.GetMouseButtonDown(LEFT_CLICK))
            {
                _selectMode = SelectionType.Movement;
            }
            if (!_mouseOverSelectionUI)
            {
                if (Input.GetMouseButtonDown(LEFT_CLICK)) CloseUIPanelAndDeselectShaman();
            }
        }

    }
    private void SelectMove()
    {
        SlowMotionManager.Instance.StartSlowMotionEffects();
        shadow.Show(_selectedShaman);
        OnShamanSelect?.Invoke(_selectedShaman);
    }
    private void ReleaseMove()
    {
        _selectMode = SelectionType.Info;
        if (!shadow.IsActive) return;
        SlowMotionManager.Instance.EndSlowMotionEffects();
        shadow.Hide();
        var newDest = GameManager.CameraHandler.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        _selectedShaman.Movement.SetDestination(newDest);
    }

    private void CancelMove()
    {
        SlowMotionManager.Instance.EndSlowMotionEffects();
        shadow.Hide();
    }

    private void CloseUIPanelAndDeselectShaman()
    {
        HeroSelectionUI.Instance.Hide();
        OnShamanDeselected?.Invoke(_selectedShaman);
        _selectedShaman = null;
    }

    public bool GetOnMouseDownShaman()
    {
        return true;
    }
}
