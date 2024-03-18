using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionHandler : MonoBehaviour, ISelection
{
    public event Action<Shaman> OnShamanMoveSelect;
    public event Action<Shaman> OnShamanInfoSelect;
    public event Action<Shaman> OnShamanDeselected;
    public SelectionType SelectMode { get; }
    public Shaman SelectedShaman { get; }
    public Shadow Shadow { get; }
    
    [SerializeField] private Shadow shadow;
    
    private const int LEFT_CLICK = 0;
    private const int RIGHT_CLICK = 1;
    private const int MIDDLE_CLICK = 2;
    private bool _mouseOverSelectionUI => HeroSelectionUI.Instance.MouseOverUI;
    private Shaman _selectedShaman;
    private SelectionType _selectMode;

    private void Start()
    {
        //_selectedShaman = LevelManager.Instance.ShamanParty[0];
        //HeroSelectionUI.Instance.Show(_selectedShaman);
    }
    public void OnShamanClick(PointerEventData.InputButton button, Shaman shaman)
    {
        if (button == PointerEventData.InputButton.Left)
        {
            _selectedShaman = shaman;
            _selectMode = SelectionType.Info;
            HeroSelectionUI.Instance.Show(shaman);
        }
    }

    private void Update()
    {
        if (ReferenceEquals(_selectedShaman, null)) return;

        if (Input.GetMouseButtonDown(RIGHT_CLICK)) SelectMove();

        if (Input.GetMouseButton(RIGHT_CLICK))
        {
            if (Input.GetMouseButtonDown(LEFT_CLICK)) CancelMove();
        }

        if (Input.GetMouseButtonUp(RIGHT_CLICK)) ReleaseMove();
    }
    private void SelectMove()
    {
        SlowMotionManager.Instance.StartSlowMotionEffects();
        shadow.Show(_selectedShaman);
        OnShamanMoveSelect?.Invoke(_selectedShaman);
    }
    private void ReleaseMove()
    {
        if(!shadow.IsActive) return;
        
        SlowMotionManager.Instance.EndSlowMotionEffects();
        shadow.Hide();
        var newDest = GameManager.Instance.CameraHandler.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        _selectedShaman.Movement.SetDestination(newDest);
        OnShamanDeselected?.Invoke(_selectedShaman);
        CloseUIPanelAndDeselectShaman();
    }

    private void CancelMove()
    {
        SlowMotionManager.Instance.EndSlowMotionEffects();
        shadow.Hide();
        OnShamanDeselected?.Invoke(_selectedShaman);
    }

    private void CloseUIPanelAndDeselectShaman()
    {
        HeroSelectionUI.Instance.Hide();
        _selectedShaman = null;
    }
}