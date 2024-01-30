using System;
using System.Collections.Generic;
using UnityEngine;


public class PowerStructure : MonoBehaviour
{
    [Header("Config File")] [SerializeField]
    private PowerStructureConfig _powerStructureConfig;

    [Header("Serialized Fields")] [SerializeField]
    private ProximityRingsManager proximityRingsManager;

    [SerializeField] private SpriteRenderer _powerStructureSpriteRenderer;
    [SerializeField] private SpriteMask _powerStructureMask;
    [Space] [SerializeField] private bool _testing;

    //private int _currentActiveRingId = 4;
    private StatType _statType;
    private Modifier _statModifier;
    private Dictionary<int, float> _activeShadowRingIds = new Dictionary<int, float>();

    public void Init()
    {
        if (_powerStructureConfig.PowerStructureSprite is null)
        {
            Debug.LogError("Config Sprite is missing");
            return;
        }

        proximityRingsManager.Init(_powerStructureConfig);
        _powerStructureSpriteRenderer.sprite = _powerStructureConfig.PowerStructureSprite;
        _powerStructureMask.sprite = _powerStructureConfig.PowerStructureSprite;
        _statType = _powerStructureConfig.statEffect.StatType;
        _statModifier = _powerStructureConfig.statEffect.Modifier;
        foreach (var ring in proximityRingsManager.RingHandlers)
        {
            ring.OnShamanEnter += OnShamanRingEnter;
            ring.OnShamanExit += OnShamanRingExit;
            ring.OnShadowEnter += OnShadowRingEnter;
            ring.OnShadowExit += OnShadowRingExit;
        }

        SelectionManager.Instance.OnShamanDeselected += OnShadowDeselect;
    }
    private void OnShadowDeselect(Shaman shaman)
    {
        proximityRingsManager.ToggleAllSprites(false);
        HideUI(SelectionManager.Instance.Shadow);
    }

    private void OnShamanRingEnter(int ringId, Shaman shaman)
    {
        if (_testing) Debug.Log($"shaman entered ring {ringId}");

        var statAdditionValue = GetStatEffectValue(ringId, shaman.Stats);
        shaman.Stats.AddValueToStat(_statType,statAdditionValue);
    }
    private void OnShamanRingExit(int ringId, Shaman shaman)
    {
        if (_testing) Debug.Log($"shaman exited ring {ringId}");
        
        var statAdditionValue = GetStatEffectValue(ringId, shaman.Stats);
        shaman.Stats.AddValueToStat(_statType,-statAdditionValue);
    }
    private void OnShadowRingEnter(int ringId, Shadow shadow)
    {
        _activeShadowRingIds.Add(ringId,_powerStructureConfig.statEffect.RingValues[ringId]);
        
        if (_testing) Debug.Log($"Shadow Enter: {ringId}");
        
        //switch between ring sprites
        var currentActiveRing = proximityRingsManager.RingHandlers[ringId];
        proximityRingsManager.ToggleAllSprites(false);
        currentActiveRing.ToggleSprite(true);
        
        var statAdditionValue = GetStatEffectValue(ringId, shadow.Stats);
        shadow.SetPSStatValue(_statType,statAdditionValue);
        
        ShowUI(shadow,ringId);
    }

    private void OnShadowRingExit(int ringId, Shadow shadow)
    {
        _activeShadowRingIds.Remove(ringId);

        if (_testing) Debug.Log($"Shadow Exit: {ringId}");

        var statAdditionValue = GetStatEffectValue(ringId, shadow.Stats);
        shadow.SetPSStatValue(_statType,-statAdditionValue);
        
        proximityRingsManager.ToggleAllSprites(false);
        if (ringId + 1 < proximityRingsManager.RingHandlers.Length)
        {
            var currentActiveRing = proximityRingsManager.RingHandlers[ringId + 1];
            currentActiveRing.ToggleSprite(true);
            ShowUI(shadow,ringId);
        }
        else
        {
            HideUI(shadow);
        }
    }
    private void ShowUI(Shadow shadow,int ringId)
    {
        HeroSelectionUI.Instance.UpdateStatBlocks(_statType, CalculateStatValueForSelectionUI(shadow,shadow.Shaman));
        StatEffectPopupManager.ShowPopupWindows(GetInstanceID(), _statType.ToString(), CalculateStatValueForPSUI(), true, GetRingColorAlpha(ringId));
    }

    private void HideUI(Shadow shadow)
    {
        StatEffectPopupManager.HidePopupWindows(GetInstanceID());
        HeroSelectionUI.Instance.UpdateStatBlocks(_statType, CalculateStatValueForSelectionUI(shadow,shadow.Shaman));
    }

    private int GetStatEffectValue(int ringId, UnitStats stats)
    {
        var value = stats.GetBaseStatValue(_statType);
        var modifier = _powerStructureConfig.statEffect.RingValues[ringId];
        
        switch (_statModifier)
        {
            case Modifier.Addition:
                return Mathf.RoundToInt(modifier);
            case Modifier.Multiplication:
                var modifiedValue = value * modifier;
                return Mathf.RoundToInt(modifiedValue);
        }
        return 0;
    }

    private int CalculateStatValueForPSUI()
    {
        float sumRingValues = 0;
        foreach (var ring in _activeShadowRingIds)
        {
            sumRingValues += ring.Value;
        }
        switch (_statModifier)
        {
            case Modifier.Addition:
                return Mathf.RoundToInt(sumRingValues);
            case Modifier.Multiplication:
                float statValue = sumRingValues * 100;
                return Mathf.RoundToInt(statValue);
        }
        return 0;
    }

    private int CalculateStatValueForSelectionUI(Shadow shadow, Shaman shaman)
    {
        var shamanStat = shaman.Stats.GetStatValue(_statType) - shaman.Stats.GetBaseStatValue(_statType);
        var shadowStat = 0;
        if (shadow.CurrentStatPSEffects.TryGetValue(_statType, out var value))
            shadowStat = value;
        
        return Mathf.RoundToInt(shadowStat - shamanStat);
    }

    private Color GetRingColorAlpha(int ringId)
    {
        Color color = _powerStructureConfig.PowerStructureTypeColor;
        float alpha = _powerStructureConfig.DefaultSpriteAlpha - _powerStructureConfig.SpriteAlphaFade * ringId;
        color.a = alpha;
        return color;
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
}
