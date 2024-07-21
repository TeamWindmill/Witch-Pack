using System;
using TMPro;
using Tools.Helpers;
using UnityEngine;
using UnityEngine.UI;


public class StatBonusPopupWindowHandler : MonoBehaviour
{
    public bool IsActive => _isActive;
    public int ActiveEntityId => _activeEntityId;
    [SerializeField] private TextMeshProUGUI _popupText;
    [SerializeField] private Image _popupVisual;
    private string _statBonusText;
    private bool _isActive;
    private int _activeEntityId = -1;

    private bool _psInfoWindow;
    private PowerStructure _powerStructure;

    private void OnValidate()
    {
        _popupText ??= GetComponentInChildren<TextMeshProUGUI>();
        _popupVisual ??= GetComponentInChildren<Image>();
    }

    public void Init()
    {
        _popupVisual.gameObject.SetActive(false);
        _popupText.gameObject.SetActive(false);
    }

    public void ShowPopupWindow(int EntityId, string statBonusText, float value, bool isPercent, Color color, float yAxisModifier)
    {
        _activeEntityId = EntityId;
        _popupVisual.gameObject.SetActive(true);
        _popupText.gameObject.SetActive(true);
        _isActive = true;
        _statBonusText = statBonusText;
        UpdateWindowPosition(yAxisModifier);
        _popupText.text = isPercent ? $"{_statBonusText} +{value}%" : $"{_statBonusText} +{value}";
        _popupVisual.color = color;
    }

    public void ShowPSInfo(PowerStructure powerStructure)
    {
        _popupVisual.gameObject.SetActive(true);
        _popupText.gameObject.SetActive(true);
        _powerStructure = powerStructure;
        _popupText.text = powerStructure.Config.statEffect.StatType.ToString().ToLowercaseNamingConvention();
        _popupVisual.color = powerStructure.Config.PowerStructureTypeColor;
        _isActive = true;
        _psInfoWindow = true;
    }

    public void UpdatePopupWindow(float value, bool isPercent, Color color)
    {
        _popupText.text = isPercent ? $"{_statBonusText} +{value}%" : $"{_statBonusText} +{value}";
        _popupVisual.color = color;
    }

    public void UpdateWindowPosition(float yAxisModifier)
    {
        transform.localPosition = new Vector3(0, yAxisModifier, 0);
    }

    public void HidePopupWindow()
    {
        transform.position = Vector3.zero;
        _popupText.text = "";
        _popupVisual.gameObject.SetActive(false);
        _popupText.gameObject.SetActive(false);
        _isActive = false;
        _psInfoWindow = false;
    }

    private void Update()
    {
        if (_isActive && _psInfoWindow)
        {
            var screenPos = GameManager.CameraHandler.MainCamera.WorldToScreenPoint(_powerStructure.InfoWindowPos.position);
            transform.position = screenPos;
        }
    }
}