using System;
using UnityEngine.EventSystems;

public interface ISelection
{
    public event Action<Shaman> OnShamanSelect;
    public event Action<Shaman> OnShamanDeselected;
    public event Action<Shadow> OnShadowSelect;
    public event Action<Shadow> OnShadowDeselected;
    public SelectionType SelectMode { get; }
    public Shaman SelectedShaman { get; }
    public Shadow Shadow { get; }
    public void OnShamanClick(PointerEventData.InputButton button, Shaman shaman);
}

