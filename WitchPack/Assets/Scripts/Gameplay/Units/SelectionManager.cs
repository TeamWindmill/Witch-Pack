using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectionManager : MonoBehaviour
{
    public event Action<Shaman> OnShamanMoveSelect;
    public event Action<Shaman> OnShamanInfoSelect;
    public event Action<Shaman> OnShamanDeselected;

    [SerializeField] private Shadow shadow;

    public SelectionType SelectMode => _selectMode;

    public Shaman SelectedShaman => _selectedShaman;

    private bool _mouseOverUI => HeroSelectionUI.Instance.MouseOverUI;
    private Shaman _selectedShaman;
    private SelectionType _selectMode;

    private void Awake()
    {
        OnShamanMoveSelect += ShamanMoveSelect;
        OnShamanInfoSelect += ShamanInfoSelect;
        OnShamanDeselected += ShamanMoveDeselect;
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
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                if (_mouseOverUI) return;
                OnShamanDeselected?.Invoke(SelectedShaman);
                _selectedShaman = null;
            }
        }

        if (SelectMode == SelectionType.Movement)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_mouseOverUI) return;

                Vector3 newDest = GameManager.Instance.CameraHandler.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                _selectedShaman.Movement.SetDest(newDest);
                OnShamanDeselected?.Invoke(SelectedShaman);
                _selectedShaman = null;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (_mouseOverUI) return;

                OnShamanDeselected?.Invoke(SelectedShaman);
                _selectedShaman = null;
            }
        }
    }

    private void ShamanMoveSelect(Shaman shaman)
    {
        SlowMotionManager.Instance.StartSlowMotionEffects();
        HeroSelectionUI.Instance.Show(shaman);
        shadow.Show(shaman);
    }

    private void ShamanInfoSelect(Shaman shaman)
    {
        HeroSelectionUI.Instance.Show(shaman);
    }

    private void ShamanMoveDeselect(Shaman shaman)
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
}

public enum SelectionType
{
    Movement,
    Info
}