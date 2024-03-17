using System;
using UnityEngine;

public class SelectionManager : MonoBehaviour, ISelection
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
    public void SetSelectedShaman(Shaman selectedShaman, SelectionType selectMode)
    {
        throw new NotImplementedException();
    }

    private void Update()
    {
        throw new NotImplementedException();
    }
}