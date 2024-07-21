using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSelectionData : MonoBehaviour
{
    public void SetSelectionDataLayout(int dataType)
    {
        SelectionData.Instance.SetSelectionLayout((SelectionLayout)dataType);

    }
}
