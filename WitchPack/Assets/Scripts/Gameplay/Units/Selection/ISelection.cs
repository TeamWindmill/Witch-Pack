using System;
using UnityEngine.EventSystems;

namespace Gameplay.Units.Selection
{
    public interface ISelection
    {
        public event Action<Shaman.Shaman> OnShamanSelect;
        public event Action<Shaman.Shaman> OnShamanDeselected;
        public event Action<Shadow.Shadow> OnShadowSelect;
        public event Action<Shadow.Shadow> OnShadowDeselected;
        public SelectionType SelectMode { get; }
        public Shaman.Shaman SelectedShaman { get; }
        public Shadow.Shadow Shadow { get; }
        public void OnShamanClick(PointerEventData.InputButton button, Shaman.Shaman shaman);
    }
}

