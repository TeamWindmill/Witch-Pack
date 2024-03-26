using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionHandler : MonoBehaviour, ISelection
{
    public event Action<Shaman> OnShamanSelect;
    public event Action<Shaman> OnShamanDeselected;
    public event Action<Shadow> OnShadowSelect;
    public event Action<Shadow> OnShadowDeselected;
    public SelectionType SelectMode { get; }
    public Shaman SelectedShaman { get; }
    public Shadow Shadow { get; }
    
    [SerializeField] private Shadow shadow;
    [SerializeField] private ParticleSystem quickMoveEffect;
    [SerializeField] private Animator quickMoveArrowsAnimator;
    
    private const int LEFT_CLICK = 0;
    private const int RIGHT_CLICK = 1;
    private const int MIDDLE_CLICK = 2;
    private bool _mouseOverSelectionUI => HeroSelectionUI.Instance.MouseOverUI;
    private Shaman _selectedShaman;
    private SelectionType _selectMode;
    [SerializeField] private float _maxHoldTime;
    private float _currentHoldTime;
    private bool _inSelectMode;

    public void OnShamanClick(PointerEventData.InputButton button, Shaman shaman)
    {
        if (button == PointerEventData.InputButton.Left)
        {
            if (_selectedShaman != null)
            {
                _selectedShaman.ShamanVisualHandler.HideShamanRing();
                _selectedShaman.IsSelected = false;
            }
            _selectedShaman = shaman;
            _selectMode = SelectionType.Info;
            HeroSelectionUI.Instance.Show(shaman);
            _selectedShaman.ShamanVisualHandler.ShowShamanRing();
            _selectedShaman.IsSelected = true;
        }
    }

    private void Update()
    {
        if (ReferenceEquals(_selectedShaman, null)) return;
        if (_currentHoldTime < _maxHoldTime)
        {
            if (Input.GetMouseButtonUp(RIGHT_CLICK))
            {
                QuickMove();
                return;
            }
        }
        if (Input.GetMouseButton(RIGHT_CLICK))
        {
            if (Input.GetMouseButtonDown(LEFT_CLICK))
            {
                CancelMove();
                _inSelectMode = false;
                _currentHoldTime = 0;
                return;
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
        }
        else
        {
            _inSelectMode = false;
            _currentHoldTime = 0;
        }

        if (Input.GetMouseButtonUp(RIGHT_CLICK)) ReleaseMove();

        if (!_mouseOverSelectionUI)
        { 
            if (Input.GetMouseButtonDown(LEFT_CLICK)) CloseUIPanelAndDeselectShaman();
        }

    }
    private void QuickMove()
    {
        var newDest = GameManager.Instance.CameraHandler.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        _selectedShaman.Movement.SetDestination(newDest);
        newDest.z = 0;
        quickMoveEffect.transform.position = newDest;
        quickMoveEffect.Play();
        quickMoveArrowsAnimator.Play("LocationArrows");
    }
    private void SelectMove()
    {
        SlowMotionManager.Instance.StartSlowMotionEffects();
        shadow.Show(_selectedShaman);
        OnShamanSelect?.Invoke(_selectedShaman);
    }
    private void ReleaseMove()
    {
        if (!shadow.IsActive) return;

        SlowMotionManager.Instance.EndSlowMotionEffects();
        shadow.Hide();
        var newDest = GameManager.Instance.CameraHandler.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        _selectedShaman.Movement.SetDestination(newDest);
        OnShadowDeselected?.Invoke(shadow);
    }
    private void CancelMove()
    {
        SlowMotionManager.Instance.EndSlowMotionEffects();
        shadow.Hide();
        OnShadowDeselected?.Invoke(shadow);
    }

    private void CloseUIPanelAndDeselectShaman()
    {
        //when pressin on something that is not the shaman or other shamans or ui make this happen
        HeroSelectionUI.Instance.Hide();
        OnShamanDeselected?.Invoke(_selectedShaman);
        _selectedShaman.ShamanVisualHandler.HideShamanRing();
        _selectedShaman.IsSelected = false;
        _selectedShaman = null;

    }

    public bool GetOnMouseDownShaman()
    {
        return false;
    }
}