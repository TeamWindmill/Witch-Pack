using UnityEngine;


public class StatEffectPopupManager : MonoBehaviour
{
    [SerializeField] private Transform _parentHolder;
    [SerializeField] private StatEffectPopupHandler _statEffectPopupHandlerPrefab;

    private static StatEffectPopupHandler _statEffectPopupHandler;

    private void Awake()
    {
        _statEffectPopupHandler = Instantiate(_statEffectPopupHandlerPrefab, _parentHolder);
    }

    public static void ShowPopupWindows(int EntityId, Transform followTransform, string statName, float value, bool isPercent, Color color)
    {
        _statEffectPopupHandler.ShowPopupWindows(EntityId, followTransform, statName, value, isPercent, color);
    }

    public static void HidePopupWindows(int powerStructureId)
    {
        _statEffectPopupHandler.HidePopupWindow(powerStructureId);
    }
}