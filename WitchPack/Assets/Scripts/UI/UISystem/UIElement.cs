using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.UISystem
{
    public class UIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        //inherit from this class if it is a ui element
        public event Action<UIElement> OnMouseEnter;
        public event Action<UIElement> OnMouseExit;
        public RectTransform RectTransform => rectTransform;
        public bool CloseOnClickOutside => closeOnClickOutside;
        public bool isMouseOver { get; private set; }
        public UIWindowManager WindowManager { get; private set; }

        [SerializeField, HideInInspector] protected RectTransform rectTransform;

        [FoldoutGroup("UI Element")] [SerializeField]
        private bool showOnAwake = false;

        [FoldoutGroup("UI Element")] [SerializeField]
        private bool hideOnAwake = false;

        [FoldoutGroup("UI Element")] [SerializeField]
        private bool assignUIGroup = false;

        [FoldoutGroup("UI Element")] [SerializeField, ShowIf(nameof(assignUIGroup))]
        protected bool isUIGroupManager;
    
        [FoldoutGroup("UI Element")] [SerializeField, ShowIf(nameof(isUIGroupManager))]
        protected bool closeOnClickOutside;
    
        [FoldoutGroup("UI Element")] [SerializeField, ShowIf(nameof(assignUIGroup))]
        protected UIGroup uiGroup;

        [FoldoutGroup("UI Element")] [SerializeField]
        private bool showInfoWindow = false;

        [FoldoutGroup("UI Element")] [SerializeField, ShowIf(nameof(showInfoWindow))]
        protected WindowInfo _windowInfo;


        protected virtual void Awake()
        {
            if (assignUIGroup)
            {
                if (isUIGroupManager)
                    UIManager.AddUIGroupManager(this, uiGroup);
                else
                    UIManager.AddUIElement(this, uiGroup);
            }

            if (showOnAwake)
                Show();
            if (hideOnAwake)
                Hide();
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Refresh()
        {
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetParent(UIWindowManager windowManager)
        {
            WindowManager = windowManager;
        }

        protected virtual void OnDestroy()
        {
            OnMouseExit?.Invoke(this);
            if (assignUIGroup)
            {
                if (isUIGroupManager)
                    UIManager.RemoveUIGroupManager(this, uiGroup);
                else
                    UIManager.RemoveUIElement(this, uiGroup);
            }
        }

        protected virtual void Update()
        {
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            isMouseOver = true;
            OnMouseEnter?.Invoke(this);
            if (showInfoWindow) InformationWindow.Instance.RequestShow(this, _windowInfo);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            isMouseOver = false;
            OnMouseExit?.Invoke(this);
            if (showInfoWindow)
            {
                if (InformationWindow.Instance.isActive) InformationWindow.Instance.Hide();
            }
        }

        protected virtual void OnDisable()
        {
            isMouseOver = false;
            OnMouseExit?.Invoke(this);
        }
    
        protected virtual void OnValidate()
        {
            rectTransform ??= GetComponent<RectTransform>();
        }
    }

    public abstract class UIElement<T> : UIElement
    {
        public bool IsInitialized { get; protected set; }

        public virtual void Init(T rootAbility)
        {
            IsInitialized = true;
        }
    }
    public abstract class UIElement<T1,T2> : UIElement
    {
        public bool IsInitialized { get; protected set; }
        public virtual void Init(T1 data1,T2 data2)
        {
            IsInitialized = true;
        }
    }
    public abstract class UIElement<T1,T2,T3> : UIElement
    {
        public bool IsInitialized { get; protected set; }
        public virtual void Init(T1 data1,T2 data2,T3 data3)
        {
            IsInitialized = true;
        }
    }
}