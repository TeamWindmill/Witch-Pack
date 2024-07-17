using UnityEngine;


public class StatBonusPopupManager : MonoBehaviour
{
    [SerializeField] private Transform _parentHolder;
    [SerializeField] private StatBonusPopupHandler _statBonusPopupHandlerPrefab;
    [SerializeField] private StatBonusPopupWindowHandler _psInfoWindowPrefab;

    private static StatBonusPopupHandler _statBonusPopupHandler;
    private static StatBonusPopupWindowHandler _psInfoWindow;

    private void Awake()
    {
        _statBonusPopupHandler = Instantiate(_statBonusPopupHandlerPrefab, _parentHolder);
        _psInfoWindow = Instantiate(_psInfoWindowPrefab, _parentHolder);
        _psInfoWindow.HidePopupWindow();
    }

    public static void ShowPopupWindows(int EntityId, Transform followTransform, string statName, float value, bool isPercent, Color color)
    {
        _statBonusPopupHandler.ShowPopupWindows(EntityId, followTransform, statName, value, isPercent, color);
    }

    public static void HidePopupWindows(int powerStructureId)
    {
        _statBonusPopupHandler.HidePopupWindow(powerStructureId);
    }

    public static void ShowPSInfoPopup(PowerStructure powerStructure)
    {
        _psInfoWindow.ShowPSInfo(powerStructure);
    }
    public static void HidePSInfoPopup()
    {
        _psInfoWindow.HidePopupWindow();
    }
}