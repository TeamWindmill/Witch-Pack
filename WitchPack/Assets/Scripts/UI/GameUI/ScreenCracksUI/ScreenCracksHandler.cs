using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ScreenCracksHandler : MonoSingleton<ScreenCracksHandler>
{
    [SerializeField] private Canvas _canvas;
    [BoxGroup("Cracks")] [SerializeField] private ScreenCrack[] _cracks;
    [BoxGroup("Cracks")] [SerializeField] private ScreenCracksVignette _cracksVignette;
    private int _hpPerCrack;
    private float _vignetteValuePerHp;
    private CoreTemple _core;
    [SerializeField] private float _pulseStrength;

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
        _cracksVignette.SetStartValue();
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
        _core = core;
        _hpPerCrack = core.Damageable.MaxHp / _cracks.Length;
        _vignetteValuePerHp = Mathf.Abs(_cracksVignette.EffectValues[0].EndValue - _cracksVignette.EffectValues[0].StartValue) / core.Damageable.MaxHp;
    }
    public void StartCracksAnimation(int damage)
    {
        var missingHP = _core.Damageable.MaxHp - _core.Damageable.CurrentHp ;
        var crackCount = missingHP / _hpPerCrack;

        for (int i = 0; i < crackCount; i++)
        {
            if(_cracks[i].ScreenCrackLerper.Finished) continue;
            _cracks[i].ScreenCrackLerper.StartTransitionEffect();
        }

        _cracksVignette.CurrentEndValue = _cracksVignette.CurrentStartValue + _vignetteValuePerHp * _pulseStrength;
        _cracksVignette.StartTransitionEffect();
        _cracksVignette.CurrentStartValue += _vignetteValuePerHp;
    }
}


