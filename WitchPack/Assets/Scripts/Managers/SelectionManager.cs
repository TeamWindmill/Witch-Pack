using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class SelectionManager : MonoBehaviour
{
    public event Action<Shaman> OnShamanMoveSelect;
    public event Action<Shaman> OnShamanInfoSelect;
    public event Action<Shaman> OnShamanDeselected;
    public SelectionType SelectMode => _selectMode;
    public Shaman SelectedShaman => _selectedShaman;
    public Shadow Shadow => shadow;

    [SerializeField] private Shadow shadow;
    private const int LEFT_CLICK = 0;
    private const int RIGHT_CLICK = 1;
    private const int MIDDLE_CLICK = 2;
    private bool _mouseOverSelectionUI => HeroSelectionUI.Instance.MouseOverUI;
    private Shaman _selectedShaman;
    private SelectionType _selectMode;


    protected void Awake()
    {
        OnShamanMoveSelect += ShamanMoveSelect;
        OnShamanInfoSelect += ShamanInfoSelect;
        OnShamanDeselected += ShamanDeselect;
       
    }
    private void Start()
    {
        HeroSelectionUI.Instance.OnMouseEnter += OnSelectionUIMouseEnter;
        HeroSelectionUI.Instance.OnMouseExit += OnSelectionUIMouseExit;
        shadow.Hide();
    }

    public void SetSelectedShaman(Shaman selectedShaman, SelectionType selectMode)
    {
        if (ReferenceEquals(selectedShaman, _selectedShaman))
        {
            return;
        }

        _selectedShaman = selectedShaman;
        _selectMode = selectMode;
        
        if (selectMode == SelectionType.Movement) OnShamanMoveSelect?.Invoke(selectedShaman);
        else if (selectMode == SelectionType.Info) OnShamanInfoSelect?.Invoke(selectedShaman);
    }

    private void Update()
    {
        if (ReferenceEquals(_selectedShaman, null)) return;

        if (SelectMode == SelectionType.Info)
        {
            if (Input.GetMouseButtonDown(LEFT_CLICK))
            {
                if (_mouseOverSelectionUI) return;
                if (_selectedShaman.MouseOverShaman)
                {
                    ShamanMoveSelect(_selectedShaman);
                    return;
                }
                OnShamanDeselected?.Invoke(SelectedShaman);
                _selectedShaman = null;
            }

            if (Input.GetMouseButtonDown(RIGHT_CLICK))
            {
                if (_mouseOverSelectionUI || _selectedShaman.MouseOverShaman) return;
                OnShamanDeselected?.Invoke(SelectedShaman);
                _selectedShaman = null;
            }
        }

        if (SelectMode == SelectionType.Movement)
        {
            
                
            if (Input.GetMouseButtonDown(LEFT_CLICK))
            {
                if (_mouseOverSelectionUI || _selectedShaman.MouseOverShaman) return;
                
                //set destination for selected shaman
                var newDest = GameManager.Instance.CameraHandler.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                _selectedShaman.Movement.SetDestination(newDest);
                OnShamanDeselected?.Invoke(SelectedShaman);
                _selectedShaman = null;
            }
            else if (Input.GetMouseButtonDown(RIGHT_CLICK))
            {
                if (_mouseOverSelectionUI) return;
                if (_selectedShaman.MouseOverShaman)
                {
                    
                    ShamanInfoSelect(_selectedShaman);
                    return;
                }
                
                OnShamanDeselected?.Invoke(SelectedShaman);
                _selectedShaman = null;
            }
        }
    }

    private void ShamanMoveSelect(Shaman shaman)
    {
        if (_selectedShaman.MouseOverShaman && _selectMode == SelectionType.Info)
        {
            _selectMode = SelectionType.Movement;
            SlowMotionManager.Instance.StartSlowMotionEffects();
            shadow.Show(shaman);
            return;
        }
        
        SlowMotionManager.Instance.StartSlowMotionEffects();
        shadow.Show(shaman);
        HeroSelectionUI.Instance.Show(shaman);
    }

    private void ShamanInfoSelect(Shaman shaman)
    {
        if (_selectedShaman.MouseOverShaman && _selectMode == SelectionType.Movement)
        {
            _selectMode = SelectionType.Info;
            SlowMotionManager.Instance.EndSlowMotionEffects();
            shadow.Hide();
        }
        
        HeroSelectionUI.Instance.Show(shaman);
    }

    private void ShamanDeselect(Shaman shaman)
    {
        if (SelectMode == SelectionType.Info)
        {
            HeroSelectionUI.Instance.Hide();
        }
        if (SelectMode == SelectionType.Movement)
        {
            SlowMotionManager.Instance.EndSlowMotionEffects();
            HeroSelectionUI.Instance.Hide();
            shadow.Hide();
        }
    }
    private void OnSelectionUIMouseEnter()
    {
        if(SelectMode != SelectionType.Movement) return;
        if(ReferenceEquals(_selectedShaman,null)) return;
        shadow.Hide();
    }
    private void OnSelectionUIMouseExit()
    {
        if(SelectMode != SelectionType.Movement) return;
        if(ReferenceEquals(_selectedShaman,null)) return;
        shadow.Show(_selectedShaman);
    }
}

public enum SelectionType
{
    None,
    Movement,
    Info
}