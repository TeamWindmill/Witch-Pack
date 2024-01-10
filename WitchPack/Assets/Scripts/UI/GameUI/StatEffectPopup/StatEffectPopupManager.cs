using UnityEngine;


public class StatEffectPopupManager : MonoBehaviour
{
    [SerializeField] private Transform _parentHolder;
    [SerializeField] private GameObject _statEffectPopupHandlerPrefab;

    private static StatEffectPopupHandler _statEffectPopupHandler;

    private void Awake()
    {
        _statEffectPopupHandler = Instantiate(_statEffectPopupHandlerPrefab, _parentHolder).GetComponent<StatEffectPopupHandler>();
    }

    public static void ShowPopupWindows(int EntityId, string statName, float value, bool isPercent, Color color)
    {
        _statEffectPopupHandler.ShowPopupWindows(EntityId, statName, value, isPercent, color);
    }

    public static void HidePopupWindows(int powerStructureId)
    {
        _statEffectPopupHandler.HidePopupWindow(powerStructureId);
    }
}