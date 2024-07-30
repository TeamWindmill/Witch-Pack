using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tools.Helpers
{
    public class ClickHelper : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler,IPointerUpHandler
    {
        public event Action<PointerEventData.InputButton> OnClick;
        public event Action OnEnterHover;
        public event Action OnExitHover;
        public event Action<PointerEventData.InputButton> OnMouseDown;
        public event Action<PointerEventData.InputButton> OnMouseUp;


        public bool IsHover { get; private set; }
        public bool IsMouseDown { get; private set; }

        private const int CLICKABLE_LAYER_INDEX = 11;

        private void Awake()
        {
            gameObject.layer = CLICKABLE_LAYER_INDEX;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(eventData.button);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            IsHover = true;
            OnEnterHover?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsHover = false;
            OnExitHover?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsMouseDown = true;
            OnMouseDown?.Invoke(eventData.button);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsMouseDown = false;
            OnMouseUp?.Invoke(eventData.button);
        }
    }
}