using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SelectionManager : MonoBehaviour
{
    private Shaman selectedShaman;
    public Action<Shaman> OnShamanSelected;
    public Action<Shaman> OnShamanDeselected;


    public Shaman SelectedShaman { get => selectedShaman; }


    private void Awake()
    {
        OnShamanSelected += ApplySelectionSlowMotion;
        OnShamanDeselected += CancelSelectonSlowMotion;
    }

    public void SetSelectedShaman(Shaman selectedShaman)
    {
        if (ReferenceEquals(selectedShaman, this.selectedShaman))
        {
            return;
        }
        this.selectedShaman = selectedShaman;
        OnShamanSelected?.Invoke(selectedShaman);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 newDest = GameManager.Instance.CameraHandler.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            if (!ReferenceEquals(selectedShaman, null))
            {
                selectedShaman.Movement.SetDest(newDest);
                OnShamanDeselected?.Invoke(SelectedShaman);
                selectedShaman = null;
            }
        }
    }

    private void ApplySelectionSlowMotion(Shaman shaman)
    {
        SlowMotionManager.Instance.StartSlowMotionEffects();
    }

    private void CancelSelectonSlowMotion(Shaman shaman)
    {
        SlowMotionManager.Instance.EndSlowMotionEffects();

    }

}
