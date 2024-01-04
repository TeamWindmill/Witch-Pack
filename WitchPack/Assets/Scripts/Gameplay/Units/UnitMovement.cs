using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitMovement : MonoBehaviour
{
    private bool reachedDest;
    private Vector2 currentDest;
    public Action<Vector3> OnDestenationSet;
    public Action<Vector3> OnDestenationReached;
    private Coroutine activeMovementRoutine;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 newDest = GameManager.Instance.CameraHandler.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            SetDest(newDest);
        }
    }

    public void SetDest(Vector3 worldPos)
    {
        currentDest = worldPos;
        reachedDest = false;
        OnDestenationSet?.Invoke(worldPos);
        if (!ReferenceEquals(activeMovementRoutine, null))
        {
            StopCoroutine(activeMovementRoutine);
        }
        activeMovementRoutine = StartCoroutine(LerpToPos());
    }

    //testing
    private IEnumerator LerpToPos()
    {
        Vector3 startPosition = transform.position;
        float counter = 0;
        while (counter <= 1)
        {
            Vector3 positionLerp = Vector3.Lerp(startPosition, currentDest, counter);
            transform.parent.position = positionLerp;
            counter += Time.deltaTime * 2;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        OnDestenationReached?.Invoke(currentDest);
        reachedDest = true;
    }


}
