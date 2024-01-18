using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public class PowerStructure : MonoBehaviour
{
    private const string POWER_STRUCTURE_LOG_GROUP = "PowerStructure";

    [Header("Config File")] [SerializeField]
    private PowerStructureConfig _powerStructureConfig;

    [Header("Serialized Fields")] [SerializeField]
    private ProximityRingsManager proximityRingsManager;

    [SerializeField] private SpriteRenderer _powerStructureSpriteRenderer;
    [SerializeField] private SpriteMask _powerStructureMask;
    [Space] [SerializeField] private bool _testing;

    private Dictionary<int, int> _activeStatEffectsOnShamans;
    private int _currentActiveRingId = 4;
    private StatType _statType;

    public void Init()
    {
        _activeStatEffectsOnShamans = new Dictionary<int, int>();

        if (_powerStructureConfig.PowerStructureSprite is null)
        {
            Debug.LogError("Config Sprite is missing");
            return;
        }

        proximityRingsManager.Init(_powerStructureConfig);
        _powerStructureSpriteRenderer.sprite = _powerStructureConfig.PowerStructureSprite;
        _powerStructureMask.sprite = _powerStructureConfig.PowerStructureSprite;
        _statType = _powerStructureConfig.statEffect.StatType;
        foreach (var ring in proximityRingsManager.RingHandlers)
        {
            ring.OnShamanEnter += OnShamanRingEnter;
            ring.OnShamanExit += OnShamanRingExit;
            ring.OnShadowEnter += OnShadowRingEnter;
            ring.OnShadowExit += OnShadowRingExit;
        }
    }

    private void OnValidate()
    {
        _powerStructureSpriteRenderer ??= GetComponentInChildren<SpriteRenderer>();
        proximityRingsManager ??= GetComponentInChildren<ProximityRingsManager>();

        if (_powerStructureConfig is null) return;

        if (_powerStructureConfig.RingsRanges.Length != proximityRingsManager.RingHandlers.Length)
        {
            Debug.LogError("the number of Rings in the SO is different than the actual rings in the prefab");
        }

        _powerStructureSpriteRenderer.sprite = _powerStructureConfig.PowerStructureSprite;
    }

    private void OnDestroy()
    {
        foreach (var ring in proximityRingsManager.RingHandlers)
        {
            ring.OnShamanEnter -= OnShamanRingEnter;
            ring.OnShamanExit -= OnShamanRingExit;
            ring.OnShadowEnter -= OnShadowRingEnter;
            ring.OnShadowExit -= OnShadowRingExit;
        }
    }

    private void OnShamanRingEnter(int ringId, Shaman shaman)
    {
        if (_testing) Debug.Log($"shaman entered ring {ringId}");

        if (_activeStatEffectsOnShamans.TryGetValue(shaman.GetInstanceID(), out var statValue))
        {
            shaman.Stats.AddValueToStat(_statType, -statValue);
            _activeStatEffectsOnShamans.Remove(shaman.GetInstanceID());
            shaman.RemovePSBonus();
        }

        var statEffectValue = GetStatEffectValue(ringId, shaman);
        shaman.Stats.AddValueToStat(_statType, statEffectValue);
        shaman.AddPSBonus(statEffectValue);
        _activeStatEffectsOnShamans.Add(shaman.GetInstanceID(), statEffectValue);
    }

    private void OnShamanRingExit(int ringId, Shaman shaman)
    {
        if (_testing) Debug.Log($"shaman exited ring {ringId}");

        if (ringId == proximityRingsManager.RingHandlers.Length - 1)
        {
            if (_activeStatEffectsOnShamans.TryGetValue(shaman.GetInstanceID(), out var statValue))
            {
                shaman.Stats.AddValueToStat(_statType, -statValue);
                _activeStatEffectsOnShamans.Remove(shaman.GetInstanceID());
                shaman.RemovePSBonus();
            }
        }
        else if (ringId < proximityRingsManager.RingHandlers.Length - 1)
        {
            if (_activeStatEffectsOnShamans.TryGetValue(shaman.GetInstanceID(), out var statValue))
            {
                shaman.Stats.AddValueToStat(_statType, -statValue);
                _activeStatEffectsOnShamans.Remove(shaman.GetInstanceID());
                shaman.RemovePSBonus();
            }

            var statEffectValue = GetStatEffectValue(ringId, shaman);
            shaman.Stats.AddValueToStat(_statType, statEffectValue);
            _activeStatEffectsOnShamans.Add(shaman.GetInstanceID(), statEffectValue);
            shaman.AddPSBonus(statEffectValue);
        }
    }

    private void OnShadowRingEnter(int ringId, Shadow shadow)
    {
        if (_testing) Debug.Log($"Shadow Enter: {ringId}");

        if (ringId < _currentActiveRingId)
        {
            var currentActiveRing = proximityRingsManager.RingHandlers[ringId];

            if (_currentActiveRingId < proximityRingsManager.RingHandlers.Length)
                proximityRingsManager.RingHandlers[_currentActiveRingId].ToggleSprite(false);
            currentActiveRing.ToggleSprite(true);
            _currentActiveRingId = currentActiveRing.Id;

            ShowStatPopupWindows(currentActiveRing, shadow);
        }
    }

    private void OnShadowRingExit(int ringId, Shadow shadow)
    {
        if (_testing) Debug.Log($"Shadow Exit: {ringId}");

        if (ringId < _currentActiveRingId) return;
        var currentActiveRing = proximityRingsManager.RingHandlers[ringId];
        proximityRingsManager.ToggleAllSprites(false);
        _currentActiveRingId = currentActiveRing.Id + 1;
        if (_currentActiveRingId > proximityRingsManager.RingHandlers.Length)
            _currentActiveRingId = proximityRingsManager.RingHandlers.Length;

        if (_currentActiveRingId >= proximityRingsManager.RingHandlers.Length)
        {
            HideStatPopupWindows(shadow);
        }
        else
        {
            proximityRingsManager.ToggleRingSprite(_currentActiveRingId, true);
            currentActiveRing = proximityRingsManager.RingHandlers[_currentActiveRingId];
            ShowStatPopupWindows(currentActiveRing, shadow);
        }
    }

    private void ShowStatPopupWindows(ProximityRingHandler ringHandler, Shadow shadow)
    {
        Color color = _powerStructureConfig.PowerStructureTypeColor;
        float alpha = _powerStructureConfig.DefaultSpriteAlpha -
                      _powerStructureConfig.SpriteAlphaFade * ringHandler.Id;
        color.a = alpha;

        var statType = _powerStructureConfig.statEffect.StatType;

        var statEffectValue = _powerStructureConfig.statEffect.RingValues[ringHandler.Id];
        var modifiedStatEffect = ModifyStatEffectForDisplay(statEffectValue, true);
        StatEffectPopupManager.ShowPopupWindows(GetInstanceID(), statType.ToString(), modifiedStatEffect, true, color);

        var newValue = CalculateStatValueForShadow(ringHandler.Id, shadow.Shaman);
        HeroSelectionUI.Instance.UpdateStatBlocks(statType, newValue);
    }

    private void HideStatPopupWindows(Shadow shadow)
    {
        StatEffectPopupManager.HidePopupWindows(GetInstanceID());


        var newValue = CalculateStatValueForShadow(-1, shadow.Shaman);
        HeroSelectionUI.Instance.UpdateStatBlocks(_statType, newValue);
    }

    private int GetStatEffectValue(int ringId, Shaman shaman)
    {
        var value = shaman.Stats.GetStatValue(_statType);
        var modifier = _powerStructureConfig.statEffect.RingValues[ringId];
        var modifiedValue = value * (modifier - 1);
        var roundedValue = Mathf.RoundToInt(modifiedValue);
        return roundedValue;
    }

    private float ModifyStatEffectForDisplay(float statEffectValue, bool rounded)
    {
        float statValue = (statEffectValue - 1) * 100;

        if (!rounded) return statValue;
        float roundedValue = MathF.Round(statValue);
        return roundedValue;
    }

    private int CalculateStatValueForShadow(int shadowRingId, Shaman shaman)
    {
        var currentStatValue = shaman.Stats.GetStatValue(_statType);
        var baseStatValue = currentStatValue;
        
        if (shaman.ShamanPSBonus.HasBonus)
        {
            baseStatValue = currentStatValue - shaman.ShamanPSBonus.BonusValue;
        }
        if (shadowRingId == -1)
            return Mathf.RoundToInt(baseStatValue);

        var modifier = _powerStructureConfig.statEffect.RingValues[shadowRingId];
        return Mathf.RoundToInt(baseStatValue * modifier);
    }
}