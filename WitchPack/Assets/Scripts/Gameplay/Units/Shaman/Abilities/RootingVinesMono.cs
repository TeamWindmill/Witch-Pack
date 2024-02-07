using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootingVinesMono : MonoBehaviour
{
    [SerializeField] private float lastingTime;
    [SerializeField] private float elapsedTime;

    private void Update()
    {
        elapsedTime += GAME_TIME.GameDeltaTime;
        if(elapsedTime >= lastingTime)
        {
            elapsedTime = 0;
            gameObject.SetActive(false);
        }
    }
}
