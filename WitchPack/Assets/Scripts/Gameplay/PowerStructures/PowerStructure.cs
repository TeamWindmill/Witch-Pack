using System.Collections.Generic;
using Gameplay.Units.Shadow;
using Gameplay.Units.Shaman;
using Gameplay.Units.Stats;
using Managers;
using UI.GameUI.HeroSelectionUI;
using UI.GameUI.StatEffectPopup;
using UnityEngine;

namespace Gameplay.PowerStructures
{
    public class PowerStructure : MonoBehaviour
    {
        public ProximityRingsManager ProximityRingsManager => proximityRingsManager;
        public PowerStructureConfig Config => _config;
        public Transform InfoWindowPos => _infoWindowPos;

        [Header("Config File")] [SerializeField]
        private PowerStructureConfig _config;

        [Header("Serialized Fields")] [SerializeField]
        private ProximityRingsManager proximityRingsManager;

        [SerializeField] private SpriteRenderer _powerStructureSpriteRenderer;
        [SerializeField] private SpriteMask _powerStructureMask;
        [SerializeField] private ParticleSystem _psEffect;
        [SerializeField] private Transform _infoWindowPos;
    
        private StatType _statType;
        private Factor _statFactor;
        private readonly Dictionary<int, float> _activeShadowRingIds = new();
        private readonly List<Shaman> _activeShamansInRings = new();

    

        public void Init()
        {
            if (_config.PowerStructureSprite is null)
            {
                Debug.LogError("Config Sprite is missing");
                return;
            }

            proximityRingsManager.Init(this);
            _powerStructureSpriteRenderer.sprite = _config.PowerStructureSprite;
            _powerStructureMask.sprite = _config.PowerStructureSprite;
            _statType = _config.statEffect.StatType;
            _statFactor = _config.statEffect.Factor;
            foreach (var ring in proximityRingsManager.RingHandlers)
            {
                ring.OnShamanEnter += OnShamanRingEnter;
                ring.OnShamanExit += OnShamanRingExit;
                ring.OnShadowEnter += OnShadowRingEnter;
                ring.OnShadowExit += OnShadowRingExit;
            }
            _psEffect.gameObject.SetActive(false);
            var main = _psEffect.main;
            main.startColor = _config.PowerStructureTypeColor;
            LevelManager.Instance.SelectionHandler.OnShadowDeselected += OnShadowDeselect;
        }
        private void OnShadowDeselect(Shadow shadow)
        {
            proximityRingsManager.ToggleAllSprites(false);
            if(ReferenceEquals(shadow,null) || ReferenceEquals(shadow.Shaman,null)) return;
            HideUI(true);
        }

        private void OnShamanRingEnter(int ringId, Shaman shaman)
        {
            var statAdditionValue = _config.statEffect.RingValues[ringId];
            shaman.Stats[_statType].AddStatValue(_statFactor,statAdditionValue);
            shaman.ActivePowerStructures[this] = ringId;
            if(!_activeShamansInRings.Contains(shaman)) _activeShamansInRings.Add(shaman);
        
            //enable effects
            //enable shaman effect
            if(!_psEffect.gameObject.activeSelf) _psEffect.gameObject.SetActive(true);
        }
        private void OnShamanRingExit(int ringId, Shaman shaman)
        {
            var statAdditionValue = _config.statEffect.RingValues[ringId];
            shaman.Stats[_statType].RemoveStatValue(_statFactor,statAdditionValue);
            if (ringId < proximityRingsManager.RingHandlers.Length - 1) shaman.ActivePowerStructures[this] = ringId + 1;
            else
            {
                shaman.ActivePowerStructures.Remove(this);
                _activeShamansInRings.Remove(shaman);

                //disable effects
                //disable shaman effect
                if(_activeShamansInRings.Count == 0) _psEffect.gameObject.SetActive(false);
            }
        }
        private void OnShadowRingEnter(int ringId, Shadow shadow)
        {
            //Debug.Log($"Shadow Enter: {ringId}");
        
            //switch between ring sprites
            if (_activeShadowRingIds.Count > 0)
            {
                foreach (var ring in _activeShadowRingIds)
                {
                    if (ringId < ring.Key)
                    {
                        proximityRingsManager.ToggleAllSprites(false);
                        proximityRingsManager.ToggleRingSprite(ringId,true);
                    }
                }
            }
            else
            {
                proximityRingsManager.ToggleAllSprites(false);
                proximityRingsManager.ToggleRingSprite(ringId,true);
            }
        
            _activeShadowRingIds.Add(ringId,_config.statEffect.RingValues[ringId]);

            var statAdditionValue = _config.statEffect.RingValues[ringId];
            shadow.SetPSStatValue(_statType,statAdditionValue);
        
            ShowUI(shadow,ringId);
        }

        private void OnShadowRingExit(int ringId, Shadow shadow)
        {
            _activeShadowRingIds.Remove(ringId);

            //Debug.Log($"Shadow Exit: {ringId}");

            var statAdditionValue = _config.statEffect.RingValues[ringId];
            shadow.SetPSStatValue(_statType,-statAdditionValue);
        
            proximityRingsManager.ToggleAllSprites(false);
            if (_activeShadowRingIds.Count > 0)
            {
                if (ringId == proximityRingsManager.RingHandlers.Length - 1)
                {
                    proximityRingsManager.ToggleRingSprite(ringId, false);
                    HideUI(false);
                    return;
                }
                proximityRingsManager.ToggleRingSprite(ringId + 1,true);
                ShowUI(shadow,ringId);
            }
            else
            {
                UpdateUI(shadow);
                HideUI(false);
            }
        }
        public void OnShamanHoverEnter(Shaman shaman, int ringId)
        {
            StatBonusPopupManager.ShowPopupWindows(GetInstanceID(),shaman.transform, _statType.ToString(), CalculateStatValueForPowerStructureUI(ringId), _config.ShowPercent, GetRingColorAlpha(ringId));
        }
        public void ShowUI(Shadow shadow,int ringId)
        {
            HeroSelectionUI.Instance.StatBlockPanel.UpdateBonusStatBlocks(_statType, CalculateStatValueForSelectionUI(shadow,shadow.Shaman));
            StatBonusPopupManager.ShowPopupWindows(GetInstanceID(), shadow.transform, _statType.ToString(), CalculateStatValueForPowerStructureUIByShadow(), _config.ShowPercent, GetRingColorAlpha(ringId));
        }

        public void UpdateUI(Shadow shadow)
        {
            HeroSelectionUI.Instance.StatBlockPanel.UpdateBonusStatBlocks(_statType,CalculateStatValueForSelectionUI(shadow,shadow.Shaman));
        }

        public void HideUI(bool hideSelection)
        {
            if(hideSelection) HeroSelectionUI.Instance.StatBlockPanel.HideStatBlocksBonus();
            StatBonusPopupManager.HidePopupWindows(GetInstanceID());
        }

        private float CalculateStatValueForPowerStructureUIByShadow()
        {
            float sumRingValues = 0;
            foreach (var ring in _activeShadowRingIds)
            {
                sumRingValues += ring.Value;
            }

            switch (_statFactor)
            {
                case Factor.Add:
                    return sumRingValues;
                case Factor.Multiply:
                    float statValue = sumRingValues * 100;
                    return Mathf.RoundToInt(statValue);
            }
            return 0;
        }
        private float CalculateStatValueForPowerStructureUI(int ringId)
        {
            float sumRingValues = 0;
            for (int i = proximityRingsManager.RingHandlers.Length - 1; i >= ringId; i--)
            {
                sumRingValues += _config.statEffect.RingValues[i];
            }

            switch (_statFactor)
            {
                case Factor.Add:
                    return sumRingValues;
                case Factor.Multiply:
                    float statValue = sumRingValues * 100;
                    return Mathf.RoundToInt(statValue);
            }
            return 0;
        }

        private float CalculateStatValueForSelectionUI(Shadow shadow, Shaman shaman)
        {
            var shamanStat = shaman.Stats.GetStatValue(_statType) - shaman.Stats.GetBaseStatValue(_statType);
            float shadowStat = 0;
            if (shadow.CurrentStatPSEffects.TryGetValue(_statType, out var value))
                shadowStat = value;
        
            return shadowStat - shamanStat;
        }

        private Color GetRingColorAlpha(int ringId)
        {
            Color color = _config.PowerStructureTypeColor;
            float alpha = _config.DefaultSpriteAlpha - _config.SpriteAlphaFade * ringId;
            color.a = alpha;
            return color;
        }
    
        private void OnValidate()
        {
            _powerStructureSpriteRenderer ??= GetComponentInChildren<SpriteRenderer>();
            proximityRingsManager ??= GetComponentInChildren<ProximityRingsManager>();

            if (_config is null) return;

            if (_config.RingsRanges.Length != proximityRingsManager.RingHandlers.Length)
            {
                Debug.LogError("the number of Rings in the SO is different than the actual rings in the prefab");
            }

            _powerStructureSpriteRenderer.sprite = _config.PowerStructureSprite;
        }
    }
}
