using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selectable : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private Shaman refShaman;

    public void OnPointerClick(PointerEventData eventData)
    {
        LevelManager.Instance.SelectionManager.SetSelectedShaman(refShaman);
    }
}
