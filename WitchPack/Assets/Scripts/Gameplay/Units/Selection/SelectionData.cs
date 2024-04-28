using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionData : MonoSingleton<SelectionData>
{
    private SelectionLayout _selectionLayout;

    public SelectionLayout SelectionLayout => _selectionLayout;

    public void SetSelectionLayout(SelectionLayout layout)
    { 
        _selectionLayout = layout;
    }
}
