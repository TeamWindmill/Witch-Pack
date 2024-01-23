using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    private Dictionary<UIGroup, List<UIElement>> _uiGroups;

    protected override void Awake()
    {
        base.Awake();
        
        _uiGroups = new Dictionary<UIGroup, List<UIElement>>();

        var uiGroupTypes = Enum.GetValues(typeof(UIGroup));
        for (int i = 0; i < uiGroupTypes.Length; i++)
        {
            _uiGroups.Add((UIGroup)i,new List<UIElement>());
        }
    }

    public void AddUIElement(UIElement element, UIGroup group)
    {
        if (_uiGroups.TryGetValue(group, out var uiElements))
        {
            uiElements.Add(element);
        }
        else
        {
            Debug.LogError("could not find UIGroup");
        }
    }
    public void RemoveUIElement(UIElement element, UIGroup group)
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

    public void InitUIElements(UIGroup uiGroup)
    {
        if (_uiGroups.TryGetValue(uiGroup,out var uiElements))
        {
            foreach (var element in uiElements)
            {
                element.Init();
            }
        }
    }

    public void ShowUIGroup(UIGroup uiGroup)
    {
        if (_uiGroups.TryGetValue(uiGroup,out var uiElements))
        {
            foreach (var element in uiElements)
            {
                element.Show();
            }
        }
    }
    public void HideUIGroup(UIGroup uiGroup)
    {
        if (_uiGroups.TryGetValue(uiGroup,out var uiElements))
        {
            foreach (var element in uiElements)
            {
                element.Hide();
            }
        }
    }

    public void UpdateUIGroup(UIGroup uiGroup)
    {
        if (_uiGroups.TryGetValue(uiGroup,out var uiElements))
        {
            foreach (var element in uiElements)
            {
                element.UpdateVisual();
            }
        }
    }
}

public enum UIGroup
{
    GameUI,
    MapUI,
    MenuUI,
    EndGameUI
}
