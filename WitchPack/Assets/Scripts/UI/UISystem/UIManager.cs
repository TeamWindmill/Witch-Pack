using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.UISystem
{
    [DefaultExecutionOrder(-1)]
    public static class UIManager
    {
        public static bool MouseOverUI { get; private set; }

        private static Dictionary<UIGroup, List<UIElement>> _uiGroups;
        private static Dictionary<UIGroup, UIElement> _uiGroupManagers;
        private static List<UIElement> _mouseOnUIElements = new List<UIElement>();

        public static void Init()
        {
            _uiGroups = new Dictionary<UIGroup, List<UIElement>>();
            _uiGroupManagers = new Dictionary<UIGroup, UIElement>();

            var uiGroupTypes = Enum.GetValues(typeof(UIGroup));
            for (int i = 0; i < uiGroupTypes.Length; i++)
            {
                _uiGroups.Add((UIGroup)i, new List<UIElement>());
            }

            for (int i = 0; i < uiGroupTypes.Length; i++)
            {
                _uiGroupManagers.Add((UIGroup)i, null);
            }
        }

        #region Add&Remove UI Elements

        public static void AddUIElement(UIElement element, UIGroup group)
        {
            if (_uiGroups.TryGetValue(group, out var uiElements))
            {
                uiElements.Add(element);
                element.OnMouseEnter += MouseOnUIEnter;
                element.OnMouseExit += MouseOnUIExit;
            }
            else
            {
                Debug.LogError("could not find UIGroup");
            }
        }

        public static void AddUIGroupManager(UIElement element, UIGroup group)
        {
            if (_uiGroupManagers[group] is not null) return;
            _uiGroupManagers[group] = element;
        }

        public static void RemoveUIElement(UIElement element, UIGroup group)
        {
            if (_uiGroups.TryGetValue(group, out var uiElements))
            {
                uiElements.Remove(element);
            }
            else
            {
                Debug.LogError("could not find UIGroup");
            }
        }

        public static void RemoveUIGroupManager(UIElement element, UIGroup group)
        {
            if(_uiGroupManagers[group] == element)
                _uiGroupManagers[group] = null;
        }

        #endregion

        #region UI Groups Functions

        public static UIElement GetUIGroupManager(UIGroup uiGroup)
        {
            return _uiGroupManagers[uiGroup];
        }

        public static void ShowUIGroup(UIGroup uiGroup)
        {
            if (_uiGroupManagers.TryGetValue(uiGroup, out var uiManager))
            {
                uiManager?.Show();
            }

            if (_uiGroups.TryGetValue(uiGroup, out var uiElements))
            {
                foreach (var element in uiElements)
                {
                    element.Show();
                }
            }
        }

        public static void RefreshUIGroup(UIGroup uiGroup)
        {
            if (_uiGroupManagers.TryGetValue(uiGroup, out var uiManager))
            {
                uiManager?.Refresh();
            }

            if (_uiGroups.TryGetValue(uiGroup, out var uiElements))
            {
                foreach (var element in uiElements)
                {
                    element.Refresh();
                }
            }
        }

        public static void HideUIGroup(UIGroup uiGroup)
        {
            if (_uiGroupManagers.TryGetValue(uiGroup, out var uiManager))
            {
                uiManager?.Hide();
            }

            if (_uiGroups.TryGetValue(uiGroup, out var uiElements))
            {
                foreach (var element in uiElements)
                {
                    element.Hide();
                }
            }
        }

        public static bool MouseOverUIGroup(UIGroup uiGroup)
        {
            bool mouseOverUI = false;
            if (_uiGroups.TryGetValue(uiGroup, out var uiElements))
            {
                foreach (var element in uiElements)
                {
                    if (element.isMouseOver) mouseOverUI = true;
                }
            }

            return mouseOverUI;
        }
        #endregion

        private static void MouseOnUIEnter(UIElement element)
        {
            MouseOverUI = true;
            _mouseOnUIElements.Add(element);
        }

        private static void MouseOnUIExit(UIElement element)
        {
            if (_mouseOnUIElements.Contains(element)) _mouseOnUIElements.Remove(element);
            if (_mouseOnUIElements.Count == 0)
                MouseOverUI = false;
        }
    }

    public enum UIGroup
    {
        GameUI,
        MapUI,
        MenuUI,
        EndGameUI,
        SelectionUI,
        TopCounterUI,
        PartyUI,
        Indicators,
        InfoWindow,
        DevTools,
        PartySelectionWindow,
        ShamanUpgradePanel,
        LevelSelection,
        UpgradeWindow,
        SettingsWindow,
    }
}