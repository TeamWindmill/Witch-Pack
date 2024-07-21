using System;
using UnityEngine.EventSystems;

public interface ISelection
{
    public event Action<Shaman> OnShamanMoveSelect;
    public event Action<Shaman> OnShamanInfoSelect;
    public event Action<Shaman> OnShamanDeselected;
    public SelectionType SelectMode { get; }
    public Shaman SelectedShaman { get; }
    public Shadow Shadow { get; }
    public void OnShamanClick(PointerEventData.InputButton button, Shaman shaman);
    public bool GetOnMouseDownShaman();
}

