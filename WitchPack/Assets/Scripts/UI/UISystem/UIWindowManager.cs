using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI.UISystem
{
    public class UIWindowManager : UIElement
    {
        [BoxGroup("UI Element/WindowManager"), SerializeField] private bool _showAllChildren = true;
        [BoxGroup("UI Element/WindowManager"), SerializeField] private bool _refreshAllChildren = true;
        [BoxGroup("UI Element/WindowManager"), SerializeField] private bool _hideAllChildren = true;
        [BoxGroup("UI Element/WindowManager"), SerializeField] private bool _autoAssignChildren;
        [BoxGroup("UI Element/WindowManager"),SerializeField] private List<UIElement> ChildUIElements = new();
        protected override void Awake()
        {
            if (_autoAssignChildren)
            {
                var children = GetComponentsInChildren<UIElement>().ToList();
                children.Remove(this);
                ChildUIElements.AddRange(children);
            }
            if(ChildUIElements.Count > 0) ChildUIElements.ForEach(child => child.SetParent(this));
            base.Awake();
        }

        public override void Show()
        {
            if(_showAllChildren) ChildUIElements.ForEach(child => child.Show());
            base.Show();
        }

        public override void Refresh()
        {
            if(_refreshAllChildren) ChildUIElements.ForEach(child => child.Refresh());
            base.Refresh();
        }

        public override void Hide()
        {
            if(_hideAllChildren) ChildUIElements.ForEach(child => child.Hide());
            base.Hide();
        }

        public T GetChildElement<T>() where T : UIElement
        {
            foreach (var element in ChildUIElements)
            {
                if (element.GetType() == typeof(T)) return element as T;
            }

            return null;
        }
    }
}