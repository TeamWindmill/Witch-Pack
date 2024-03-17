using System;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public ISelection ActiveSelectionHandler
    {
        get
        {
            return _activeLayout switch
            {
                SelectionLayout.OldSelectionLayout => _oldSelectionHandler,
                SelectionLayout.NewSelectionLayout => _selectionHandler,
                _ => null
            };
        }
    }

    [HideInInspector][SerializeField] private OldSelectionHandler _oldSelectionHandler;
    [HideInInspector][SerializeField] private SelectionHandler _selectionHandler;
    [SerializeField] private SelectionLayout _activeLayout;

    private void OnValidate()
    {
        _oldSelectionHandler ??= GetComponent<OldSelectionHandler>();
        _selectionHandler ??= GetComponent<SelectionHandler>();
        
        switch (_activeLayout)
        {
            case SelectionLayout.OldSelectionLayout:
                _oldSelectionHandler.enabled = true;
                _selectionHandler.enabled = false;
                break;
            case SelectionLayout.NewSelectionLayout:
                _oldSelectionHandler.enabled = false;
                _selectionHandler.enabled = true;
                break;
        }
    }
}
public enum SelectionLayout
{
    OldSelectionLayout,
    NewSelectionLayout,
}