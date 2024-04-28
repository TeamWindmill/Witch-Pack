using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SortingGroup))]
public class DynamicSortingGroupLayerSetter : MonoBehaviour
{
    [SerializeField] private bool _updateInRuntime;
    private SortingGroup _sortingGroup;
    
    private void Start()
    {
        _sortingGroup ??= GetComponent<SortingGroup>();
        _sortingGroup.sortingOrder = (int)(transform.position.y * -100);
    }

    private void Update()
    {
        if (_updateInRuntime)
        {
            _sortingGroup.sortingOrder = (int)(transform.position.y * -100);
        }

    }
}
