using Tools.Helpers;

namespace Gameplay.Units.Selection
{
    public class SelectionData : MonoSingleton<SelectionData>
    {
        private SelectionLayout _selectionLayout;

        public SelectionLayout SelectionLayout => _selectionLayout;

        public void SetSelectionLayout(SelectionLayout layout)
        { 
            _selectionLayout = layout;
        }
    }
}
