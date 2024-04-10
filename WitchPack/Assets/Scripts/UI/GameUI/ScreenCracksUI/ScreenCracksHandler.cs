using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ScreenCracksHandler : MonoSingleton<ScreenCracksHandler>
{
    [SerializeField] private Canvas _canvas;
    [BoxGroup("Cracks")] [SerializeField] private ScreenCrack[] _cracks;
    [BoxGroup("Cracks")] [SerializeField] private ScreenCracksVignette _cracksVignette;
    private int _crackIndicator;
    private int _cracksPerHp;
    private float _vignetteValuePerHp;
    
    private void Start()
    {
        _canvas.worldCamera = GameManager.Instance.CameraHandler.MainCamera;
        _canvas.sortingLayerName = "Game UI";
        SetStartValue();
    }

    public void SetStartValue()
    {
        foreach (var crack in _cracks)
        {
            crack.ScreenCrackLerper.SetStartValue();
        }
    }

    private void OnDisable()
    {
        foreach (var crack in _cracks)
        {
            crack.ScreenCrackLerper.SetStartValue();
        }
    }

    public void InitByCore(CoreTemple core)
    {
        _cracksPerHp = core.Damageable.MaxHp / _cracks.Length;
        _vignetteValuePerHp = Mathf.Abs(_cracksVignette.EffectValues[0].EndValue - _cracksVignette.EffectValues[0].StartValue) / core.Damageable.MaxHp;
    }
    public void StartCracksAnimation(int damage)
    {
        for (int i = 0; i < damage; i++)
        {
            for (int j = 0; j < _cracksPerHp; j++)
            {
                _cracks[_crackIndicator].ScreenCrackLerper.StartTransitionEffect();
                _crackIndicator++;
            }
            _cracksVignette.StartTransitionEffect();
            _cracksVignette.CurrentStartValue += _vignetteValuePerHp;
        }
    }
}


