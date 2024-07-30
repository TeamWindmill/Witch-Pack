using System.Collections.Generic;
using Animations;
using Configs;
using Gameplay.PowerStructures;
using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities;
using Gameplay.Units.Abilities.AbilitySystem.BaseConfigs;
using Gameplay.Units.Abilities.Shaman_Abilities;
using Gameplay.Units.Energy_Exp;
using Gameplay.Units.Stats;
using Gameplay.Units.Visual;
using Gameplay.Wave.Indicator;
using Managers;
using Sirenix.OdinInspector;
using Sound;
using Tools.Helpers;
using UnityEngine;
using UnityEngine.EventSystems;
using Visual.Indicator;

namespace Gameplay.Units
{
    public class Shaman : BaseUnit
    {
        #region public

        public override Stats.Stats BaseStats => ShamanConfig.BaseStats;
        public ShamanConfig ShamanConfig { get; private set; }
        public ShamanSaveData SaveData { get; private set; }
        public ShamanAbilityHandler ShamanAbilityHandler { get; private set; }
        public bool MouseOverShaman => clicker.IsHover;
        public ShamanVisualHandler ShamanVisualHandler => shamanVisualHandler;
        public Dictionary<PowerStructure, int> ActivePowerStructures { get; } = new();

        public ClickHelper Clicker => clicker;

        #endregion

        #region serialized

        [SerializeField, TabGroup("Visual")] private ShamanAnimator shamanAnimator;
        [SerializeField, TabGroup("Visual")] private ShamanVisualHandler shamanVisualHandler;
        [SerializeField] private ClickHelper clicker;
        [SerializeField] private Indicatable indicatable;
        [SerializeField] private ParticleSystem levelUpEffect;
        public ShamanEnergyHandler EnergyHandler { get; private set; }

        #endregion

        private void OnValidate()
        {
            shamanAnimator ??= GetComponentInChildren<ShamanAnimator>();
        }

        public void Init(ShamanSaveData saveData)
        {
            SaveData = saveData;
            ShamanConfig = saveData.Config;
            base.Init(ShamanConfig);
        
            ShamanAbilityHandler = new ShamanAbilityHandler(this);
            AbilityHandler = ShamanAbilityHandler;
            ShamanAbilityHandler.IntializeAbilities();
            ShamanAbilityHandler.AddMetaUpgrades(saveData);
        
            Damageable.Init();
            EnergyHandler = new ShamanEnergyHandler(this);
            EnemyTargeter.SetRadius(Stats[StatType.BaseRange].Value);
            shamanAnimator.Init(this);
            indicatable.Init(ShamanConfig.UnitIndicatorIcon, action: FocusCameraOnShaman, clickable: true,
                indicatorPointerSprite: IndicatorPointerSpriteType.Cyan);
            Indicator newIndicator = LevelManager.Instance.IndicatorManager.CreateIndicator(indicatable);
            newIndicator.gameObject.SetActive(false);
            shamanVisualHandler.Init(this, saveData.Config);
            AutoCaster.Init(this, true);

            #region Events

            // no need to unsubscribe because shaman gets destroyed between levels
            shamanVisualHandler.OnSpriteFlip += shamanAnimator.FlipAnimations;
            Movement.OnDestinationSet += AutoCaster.DisableCaster;
            Movement.OnDestinationReached += AutoCaster.EnableCaster;
            clicker.OnClick += SetSelectedShaman;
            clicker.OnEnterHover += ShamanHoveredEntered;
            clicker.OnExitHover += ShamanHoveredExit;
            //DamageDealer.OnKill += energyHandler.OnEnemyKill;
            //DamageDealer.OnAssist += energyHandler.OnEnemyAssist;
            EnergyHandler.OnShamanLevelUp += OnLevelUpGFX;
            Damageable.OnHitGFX += OnHitSFX;
            Damageable.OnDeathGFX += DeathSFX;
            Damageable.OnDeathGFX += SetOffIndicator;
            Effectable.OnAffected += ShamanVisualHandler.EffectHandler.PlayEffect;
            Effectable.OnEffectRemoved += ShamanVisualHandler.EffectHandler.DisableEffect;
            AutoCaster.CastTimeStartVFX += ShamanVisualHandler.EffectHandler.PlayEffect;
            AutoCaster.CastTimeEndVFX += ShamanVisualHandler.EffectHandler.DisableEffect;
            AutoCaster.CastTimeStart += ShamanCastSFX;
            Stats[StatType.BaseRange].OnStatChange += EnemyTargeter.SetRadius;

            #endregion

            Initialized = true;
        }

        #region Selection

        public void SetSelectedShaman(PointerEventData.InputButton button)
        {
            LevelManager.Instance.SelectionHandler.OnShamanClick(button, this);
        }

        private void ShamanHoveredEntered()
        {
            shamanVisualHandler.ShowShamanRange();
            foreach (var powerStructure in ActivePowerStructures)
            {
                powerStructure.Key.ProximityRingsManager.ToggleRingSprite(powerStructure.Value, true);
                powerStructure.Key.OnShamanHoverEnter(this, powerStructure.Value);
            }
        }

        private void ShamanHoveredExit()
        {
            shamanVisualHandler.HideShamanRange();
            foreach (var powerStructure in LevelManager.Instance.CurrentLevel.PowerStructures)
            {
                powerStructure.ProximityRingsManager.ToggleAllSprites(false);
                powerStructure.HideUI(true);
            }
        }

        public void ToggleClicker(bool state)
        {
            clicker.enabled = state;
        }

        #endregion

        #region SFX

        private void OnLevelUpGFX(int obj)
        {
            levelUpEffect.Play();
            SoundManager.PlayAudioClip(SoundEffectType.ShamanLevelUp);
        }

        private void OnHitSFX(bool isCrit)
        {
            switch (ShamanConfig.Sex)
            {
                case Sex.Male:
                    SoundManager.PlayAudioClip(SoundEffectType.ShamanGetHitMale);
                    break;
                case Sex.Female:
                    SoundManager.PlayAudioClip(SoundEffectType.ShamanGetHitFemale);
                    break;
            }

            shamanVisualHandler.HitEffect.Play();
        }

        private void DeathSFX()
        {
            switch (ShamanConfig.Sex)
            {
                case Sex.Male:
                    SoundManager.PlayAudioClip(SoundEffectType.ShamanDeathMale);
                    break;
                case Sex.Female:
                    SoundManager.PlayAudioClip(SoundEffectType.ShamanDeathFemale);
                    break;
            }
        }

        public void AttackSFX() => SoundManager.PlayAudioClip(SoundEffectType.BasicAttack);

        public void ShamanAbilityCastSFX(CastingAbilitySO abilitySo) =>
            SoundManager.PlayAudioClip(abilitySo.SoundEffectType);

        public void ShamanCastSFX(CastingAbility ability) =>
            SoundManager.PlayAudioClip(SoundEffectType.ShamanCast);

        #endregion

        #region Indicator

        private void SetOffIndicator()
        {
            indicatable.ToggleIndicatableRendering(false);
        }

        private void FocusCameraOnShaman()
        {
            GameManager.CameraHandler.SetCameraPosition(transform.position, false);
        }

        #endregion
    }
}