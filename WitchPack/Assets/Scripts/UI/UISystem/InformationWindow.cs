using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InformationWindow : MonoSingleton<InformationWindow>
{
    [SerializeField] private TextMeshProUGUI _titleTMP;
    [SerializeField] private TextMeshProUGUI _discriptionTMP;
    [Space] 
    [SerializeField] private Vector2 _windowSize;

    private RectTransform _rectTransform;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void Show(UIElement uiElement, WindowInfo windowInfo)
    {
        _rectTransform.rect.Set(0,0,_windowSize.x,_windowSize.y);
        
        _titleTMP.text = windowInfo.Name;
        _discriptionTMP.text = windowInfo.Discription;

        var uiElementRect = uiElement.RectTransform.rect;
        var windowPos = uiElement.transform.position;
        windowPos.x += uiElementRect.width/2;
        windowPos.y += uiElementRect.height/2;
        transform.position = windowPos;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        _rectTransform ??= GetComponent<RectTransform>();
    }
}

[Serializable]
public struct WindowInfo
{
    public string Name;
    public string Discription;
}
