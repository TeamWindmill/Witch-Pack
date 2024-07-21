using System;
using System.Collections.Generic;
using TMPro;
using Tools.Helpers;
using UnityEngine;


public class ProximityRingsManager : MonoBehaviour
{
    public ProximityRingHandler[] RingHandlers => _ringHandlers;
    [SerializeField] private ProximityRingHandler[] _ringHandlers;
    [SerializeField] private ClickHelper _clickHelper;
    private bool _lockSpriteToggle;
    private Color _defaultColor;
    private Color _powerStructureTypeColor;
    private bool _shamanSelected;
    private PowerStructureStatEffect _statEffect;

    private Dictionary<int, IDisposable> _activeStatusEffectOnShaman;
    private PowerStructure _powerStructure;


    public void Init(PowerStructure powerStructure)
    {
        _powerStructure = powerStructure;
        var powerStructureConfig = powerStructure.Config;
        float ringSpriteAlpha = powerStructureConfig.DefaultSpriteAlpha;
        for (int i = 0; i < _ringHandlers.Length; i++)
        {
            _ringHandlers[i].Init(i, ringSpriteAlpha);

            ringSpriteAlpha -= powerStructureConfig.SpriteAlphaFade;
        }

        _defaultColor = powerStructureConfig.RingDefaultColor;
        _powerStructureTypeColor = powerStructureConfig.PowerStructureTypeColor;
        _statEffect = powerStructureConfig.statEffect;
        _clickHelper.OnEnterHover += ActivateRingSprites;
        _clickHelper.OnExitHover += DeactivateRingSprites;
        LevelManager.Instance.SelectionHandler.OnShamanSelect += OnShamanSelect;
        LevelManager.Instance.SelectionHandler.OnShamanDeselected += OnShamanDeselect;
        ScaleCircles(powerStructureConfig.Range, powerStructureConfig.RingsRanges);
        ChangeAllRingsColors(_powerStructureTypeColor);
    }

    private void OnDestroy()
    {
        _clickHelper.OnEnterHover -= ActivateRingSprites;
        _clickHelper.OnExitHover -= DeactivateRingSprites;
        if (ReferenceEquals(LevelManager.Instance,null)) return;
        LevelManager.Instance.SelectionHandler.OnShamanSelect -= OnShamanSelect;
        LevelManager.Instance.SelectionHandler.OnShamanDeselected -= OnShamanDeselect;
    }

    private void ChangeAllRingsColors(Color color)
    {
        foreach (var ring in _ringHandlers)
        {
            ring.ChangeColor(color);
        }
    }

    private void ScaleCircles(float circleRange, float[] ringsRanges)
    {
        for (int i = 0; i < _ringHandlers.Length; i++)
        {
            _ringHandlers[i].Scale(circleRange * ringsRanges[i]);
        }
    }

    private void OnShamanSelect(Shaman shaman)
    {
        _shamanSelected = true;
    }

    private void OnShamanDeselect(Shaman shaman)
    {
        _shamanSelected = false;
    }

    #region SpriteActivationToggle

    public void ToggleAllSprites(bool state)
    {
        foreach (var ring in _ringHandlers)
        {
            ring.ToggleSprite(state);
        }
    }

    private void ActivateRingSprites()
    {
        if (_shamanSelected) return;
        ToggleRingSprite(_ringHandlers.Length-1,true);
        StatBonusPopupManager.ShowPSInfoPopup(_powerStructure);
    }

    private void DeactivateRingSprites()
    {
        if (_shamanSelected) return;
        ToggleAllSprites(false);
        StatBonusPopupManager.HidePSInfoPopup();

    }

    public void ToggleRingSprite(int ringId, bool state)
    {
        _ringHandlers[ringId].ToggleSprite(state);
    }

    #endregion
}