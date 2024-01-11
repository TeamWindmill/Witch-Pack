using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private Shaman selectedShaman;



    public void SetSelectedShaman(Shaman selectedShaman)
    {
        this.selectedShaman = selectedShaman;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 newDest = GameManager.Instance.CameraHandler.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            if (!ReferenceEquals(selectedShaman, null))
            {
                selectedShaman.Movement.SetDest(newDest);
            }
        }
    }

}
