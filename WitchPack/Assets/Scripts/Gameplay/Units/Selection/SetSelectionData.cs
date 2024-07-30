using UnityEngine;

namespace Gameplay.Units.Selection
{
    public class SetSelectionData : MonoBehaviour
    {
        private void Start()
        {
            SelectionData.Instance.SetSelectionLayout(SelectionLayout.RTSLayout);
        }
        public void SetSelectionDataLayout(int dataType)
        {
            SelectionData.Instance.SetSelectionLayout((SelectionLayout)dataType);

        }
    }
}
