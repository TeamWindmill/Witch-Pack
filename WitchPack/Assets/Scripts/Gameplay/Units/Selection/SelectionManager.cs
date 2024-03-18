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
                SelectionLayout.SelectionLayout3 => _selectionHandler3,
                
                _ => null
            };
        }
    }

    [HideInInspector][SerializeField] private OldSelectionHandler _oldSelectionHandler;
    [HideInInspector][SerializeField] private SelectionHandler _selectionHandler;
    [HideInInspector][SerializeField] private SelectionHandler3 _selectionHandler3;
    [SerializeField] private SelectionLayout _activeLayout;

    private void OnValidate()
    {
        _oldSelectionHandler ??= GetComponent<OldSelectionHandler>();
        _selectionHandler ??= GetComponent<SelectionHandler>();
        _selectionHandler3 ??= GetComponent<SelectionHandler3>();
        
        switch (_activeLayout)
        {
            case SelectionLayout.OldSelectionLayout:
                _oldSelectionHandler.enabled = true;
                _selectionHandler.enabled = false;
                _selectionHandler3.enabled = false;
                break;
            case SelectionLayout.NewSelectionLayout:
                _oldSelectionHandler.enabled = false;
                _selectionHandler.enabled = true;
                _selectionHandler3.enabled = false;
                break;
            case SelectionLayout.SelectionLayout3:
                _oldSelectionHandler.enabled = false;
                _selectionHandler.enabled = false;
                _selectionHandler3.enabled = true;
                break;
        }
    }
}
public enum SelectionLayout
{
    OldSelectionLayout,
    NewSelectionLayout,
    SelectionLayout3
}
public enum SelectionType
{
    None,
    Movement,
    Info
}