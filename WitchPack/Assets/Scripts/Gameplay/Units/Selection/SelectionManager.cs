using UnityEngine;

namespace Gameplay.Units.Selection
{
    public class SelectionManager : MonoBehaviour
    {
        public ISelection ActiveSelectionHandler
        {
            get
            {
                return _activeLayout switch
                {
                    SelectionLayout.DefaultLayout => _oldSelectionHandler,
                    SelectionLayout.RTSLayout => _selectionHandler,
                    SelectionLayout.DragLayout => _selectionHandler3,
                
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
                case SelectionLayout.DefaultLayout:
                    _oldSelectionHandler.enabled = true;
                    _selectionHandler.enabled = false;
                    _selectionHandler3.enabled = false;
                    break;
                case SelectionLayout.RTSLayout:
                    _oldSelectionHandler.enabled = false;
                    _selectionHandler.enabled = true;
                    _selectionHandler3.enabled = false;
                    break;
                case SelectionLayout.DragLayout:
                    _oldSelectionHandler.enabled = false;
                    _selectionHandler.enabled = false;
                    _selectionHandler3.enabled = true;
                    break;
            }
        }
        private void Awake()
        {
            GetLayout();
        }
        private void GetLayout()
        {
            _activeLayout = SelectionData.Instance.SelectionLayout;
            switch (_activeLayout)
            {
                case SelectionLayout.DefaultLayout:
                    _oldSelectionHandler.enabled = true;
                    _selectionHandler.enabled = false;
                    _selectionHandler3.enabled = false;
                    break;
                case SelectionLayout.RTSLayout:
                    _oldSelectionHandler.enabled = false;
                    _selectionHandler.enabled = true;
                    _selectionHandler3.enabled = false;
                    break;
                case SelectionLayout.DragLayout:
                    _oldSelectionHandler.enabled = false;
                    _selectionHandler.enabled = false;
                    _selectionHandler3.enabled = true;
                    break;
            }
        }
    }
    public enum SelectionLayout
    {
        DefaultLayout,
        RTSLayout,
        DragLayout
    }
    public enum SelectionType
    {
        None,
        Movement,
        Info
    }
}