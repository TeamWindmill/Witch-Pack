using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ScreenCracksHandler : MonoSingleton<ScreenCracksHandler>
{
    [SerializeField] private Canvas _canvas;
    [BoxGroup("Cracks")] [SerializeField] private ScreenCrack[] _cracks;
    
    
    private void Start()
    {
        _canvas.worldCamera = GameManager.Instance.CameraHandler.MainCamera;
        _canvas.sortingLayerName = "Game UI";
        foreach (var crack in _cracks)
        {
            crack.ScreenCrackLerper.SetStartValue();
        }
    }

    public void StartCracksAnimation(int crackNum)
    {
        _cracks[crackNum].ScreenCrackLerper.StartTransitionEffect();
    }

    
}


