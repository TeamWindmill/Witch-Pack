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

        var ringStatValue = _powerStructureConfig.statEffect.RingValues[ringId];

        if (_activeStatEffectsOnShamans.TryGetValue(shaman.GetInstanceID(), out var statValue))
        {
            shaman.Stats.AddValueToStat(_statType, -statValue);
        }

        _activeStatEffectsOnShamans.Add(shaman.GetInstanceID(), ringStatValue);
        shaman.Stats.AddValueToStat(_statType, ringStatValue);
    }

    private void OnShamanRingExit(int ringId, Shaman shaman)
    {
        if (_testing) Debug.Log($"shaman exited ring {ringId}");

        if (ringId == proximityRingsManager.RingHandlers.Length - 1)
        {
            if (_activeStatEffectsOnShamans.TryGetValue(shaman.GetInstanceID(), out var statValue))
            {
                shaman.Stats.AddValueToStat(_statType, -statValue);
            }
        }
        else if (ringId < proximityRingsManager.RingHandlers.Length - 1)
        {
            var ringStatValue = _powerStructureConfig.statEffect.RingValues[ringId + 1];

            if (_activeStatEffectsOnShamans.TryGetValue(shaman.GetInstanceID(), out var statValue))
            {
                shaman.Stats.AddValueToStat(_statType, -statValue);
            }

            shaman.Stats.AddValueToStat(_statType, ringStatValue);
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
        //     Color color = _powerStructureConfig.PowerStructureTypeColor;
        //     float alpha = _powerStructureConfig.DefaultSpriteAlpha -
        //                   _powerStructureConfig.SpriteAlphaFade * ringHandler.Id;
        //     color.a = alpha;
        //
        //     var shamanStat =
        //         shaman.EntityStatComponent.GetStat((int)_powerStructureConfig.StatEffectConfig.AffectedStatType);
        //
        //     var statEffectValue = GetStatEffectByRing(ringHandler);
        //     var modifiedStatEffect = ModifyStatEffectForDisplay(shamanStat, statEffectValue, true);
        //     StatEffectPopupManager.ShowPopupWindows(EntityInstanceID, shamanStat, modifiedStatEffect, true, color);
        //
        //     var changedValue = GetDeltaFromStatEffectOnShaman(statEffectValue, shamanStat, true);
        //     HeroSelectionUI.Instance.UpdateStatBlocks(shamanStat, changedValue);
    }

    private void HideStatPopupWindows(Shadow shadow)
    {
        //     StatEffectPopupManager.HidePopupWindows(EntityInstanceID);
        //
        //     var shamanStat =
        //         shaman.EntityStatComponent.GetStat((int)_powerStructureConfig.StatEffectConfig.AffectedStatType);
        //
        //     var changedValue = shamanStat.BaseValue - shamanStat.CurrentValue;
        //     var roundedValue = Mathf.Round(changedValue);
        //     HeroSelectionUI.Instance.UpdateStatBlocks(shamanStat, roundedValue);
        // }

        // private float GetStatEffectByRing(ProximityRingHandler ringHandler)
        // {
        //     float statEffectModifiedValue =
        //         _powerStructureConfig.StatEffectConfig.StatModifier.RingModifiers[ringHandler.Id];
        //
        //     return statEffectModifiedValue;
    }

    // private float ModifyStatEffectForDisplay(Stat shamanStat, float modifiedStatValue, bool rounded)
    // {
    //     float statValue = 0;
    //     float statPercent = 0;
    //     switch ((Constant.StatsId)shamanStat.Id)
    //     {
    //         case Constant.StatsId.CritChance:
    //             statPercent = (modifiedStatValue * shamanStat.BaseValue) / shamanStat.BaseValue;
    //             statValue = (statPercent - 1) * 100;
    //             break;
    //         case Constant.StatsId.AttackDamage:
    //             statPercent = (modifiedStatValue * shamanStat.BaseValue) / shamanStat.BaseValue;
    //             statValue = (statPercent - 1) * 100;
    //             break;
    //         case Constant.StatsId.CritDamage:
    //             statPercent = (modifiedStatValue * shamanStat.BaseValue) / shamanStat.BaseValue;
    //             statValue = (statPercent - 1) * 100;
    //             break;
    //     }
    //
    //
    //     if (!rounded) return statValue;
    //     float roundedValue = MathF.Round(statValue);
    //     return roundedValue;
    // }

    // private float GetDeltaFromStatEffectOnShaman(float StatEffectValue, Stat shamanStat, bool rounded)
    // {
    //     float value = StatEffectValue;
    //     switch (_powerStructureConfig.StatEffectConfig.StatModifier.StatusModifierType)
    //     {
    //         case StatusModifierType.Addition:
    //             break;
    //         case StatusModifierType.Reduce:
    //             break;
    //         case StatusModifierType.Multiplication:
    //             value = (shamanStat.BaseValue * StatEffectValue) - shamanStat.CurrentValue;
    //             break;
    //     }
    //
    //     if (!rounded) return value;
    //     float roundedValue = MathF.Round(value);
    //     return roundedValue;
    // }
}