using System.Collections.Generic;
using UnityEngine;


public class StatBonusPopupHandler : MonoBehaviour
{
    [SerializeField] private StatBonusPopupWindowHandler[] _popupWindowHandlers;
    [SerializeField] private float StatEffectPopupWindowsDistance;
    [SerializeField] private Vector3 offsetfollowPos;

    private Transform _followPosition;
    private List<StatBonusPopupWindowHandler> _activeWindowHandlers = new();
    private bool _isActive;
    private void Awake()
    {
        foreach (var popupWindowHandler in _popupWindowHandlers)
        {
            popupWindowHandler.Init();
        }
    }

    private void Update()
    {
        if (_isActive)
        {
            var screenPos = GameManager.CameraHandler.MainCamera.WorldToScreenPoint(_followPosition.position + offsetfollowPos);
            transform.position = screenPos;
        }
    }

    public void ShowPopupWindows(int EntityId, Transform followTransform, string statBonusText, float value, bool isPercent, Color color)
    {
        _followPosition = followTransform;
        _isActive = true;
        foreach (var handler in _popupWindowHandlers)
        {
            if (handler.IsActive && handler.ActiveEntityId == EntityId)
            {
                handler.UpdatePopupWindow(value, isPercent, color);
                return;
            }
        }
        for (var i = 0; i < _popupWindowHandlers.Length; i++)
        {
            if (_popupWindowHandlers[i].IsActive) continue;
            _popupWindowHandlers[i].ShowPopupWindow(EntityId, statBonusText, value, isPercent, color, _activeWindowHandlers.Count * StatEffectPopupWindowsDistance);
            _activeWindowHandlers.Add(_popupWindowHandlers[i]);
            return;
        }
    }

    public void HidePopupWindow(int powerStructureId)
    {
        _isActive = false;
        foreach (var popupWindowHandler in _popupWindowHandlers)
        {
            if (popupWindowHandler.ActiveEntityId == powerStructureId && popupWindowHandler.IsActive)
            {
                popupWindowHandler.HidePopupWindow();
                _activeWindowHandlers.Remove(popupWindowHandler);
            }
        }

        var i = 0;
        foreach (var handler in _activeWindowHandlers)
        {
            if (!handler.IsActive) continue;
            handler.UpdateWindowPosition(i * StatEffectPopupWindowsDistance);
            _isActive = true;
            i++;
        }
    }
}