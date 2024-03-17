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
    [SerializeField] private GameObject _infoWindow;
    [SerializeField] private SpriteRenderer _infoWindowBG;
    [SerializeField] private TextMeshPro _infoWindowText;
    private bool _lockSpriteToggle;
    private Color _defaultColor;
    private Color _powerStructureTypeColor;
    private bool _shamanSelected;
    private PowerStructureStatEffect _statEffect;

    private Dictionary<int, IDisposable> _activeStatusEffectOnShaman;



    public void Init(PowerStructureConfig powerStructureConfig)
    {
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
        LevelManager.Instance.OldSelectionHandler.OnShamanMoveSelect += OnShamanSelect;
        LevelManager.Instance.OldSelectionHandler.OnShamanDeselected += OnShamanDeselect;
        ScaleCircles(powerStructureConfig.Range, powerStructureConfig.RingsRanges);
        ChangeAllRingsColors(_powerStructureTypeColor);
    }

    private void OnDestroy()
    {
        _clickHelper.OnEnterHover -= ActivateRingSprites;
        _clickHelper.OnExitHover -= DeactivateRingSprites;
        if (ReferenceEquals(LevelManager.Instance,null)) return;
        LevelManager.Instance.OldSelectionHandler.OnShamanMoveSelect -= OnShamanSelect;
        LevelManager.Instance.OldSelectionHandler.OnShamanDeselected -= OnShamanDeselect;
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
        ToggleOuterRingSprite(true);
        _infoWindowBG.color = _powerStructureTypeColor;
        _infoWindowText.text = _statEffect.StatType.ToString().ToLowercaseNamingConvention();
        _infoWindow.SetActive(true);
    }

    private void DeactivateRingSprites()
    {
        if (_shamanSelected) return;
        ToggleAllSprites(false);
        _infoWindow.SetActive(false);
    }

    private void ToggleOuterRingSprite(bool state)
    {
        _ringHandlers[^1].ToggleSprite(state);
    }

    #endregion
}