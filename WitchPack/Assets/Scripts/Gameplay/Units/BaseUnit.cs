using System.Collections.Generic;
using Configs;
using Gameplay.Targeter;
using Gameplay.Units.Abilities;
using Gameplay.Units.Abilities.AbilitySystem.Casting;
using Gameplay.Units.Damage_System;
using Gameplay.Units.Movement;
using Gameplay.Units.Stats;
using Managers;
using Sirenix.OdinInspector;
using Tools.Time;
using UI.MapUI.MetaUpgrades.UpgradePanel.Configs;
using UnityEngine;
using Visual.HpBar;

namespace Gameplay.Units
{
    public class BaseUnit : BaseEntity , IDamagable
    {
        #region Public

        public bool Initialized { get; protected set; }
        public bool IsDead { get; private set; }
        public AbilityHandler AbilityHandler { get; protected set; }
        public BaseUnitConfig UnitConfig { get; private set; }
        public Damageable Damageable { get; private set; }
        public DamageDealer DamageDealer { get; private set; }
        public Affector Affector { get; private set; }
        public Effectable Effectable { get; private set; }
        public UnitTargetHelper<Shaman.Shaman> ShamanTargetHelper { get; private set; }
        public UnitTargetHelper<Enemy.Enemy> EnemyTargetHelper { get; private set; }
        public List<ITimer> UnitTimers { get; private set; }
        public HP_Bar HpBar => hpBar;
        public virtual Stats.Stats BaseStats => null;
        public UnitStats Stats => stats;
        public UnitAutoCaster AutoCaster => _autoCaster;
        public UnitMovement Movement => movement;
        public Transform CastPos => _castPos;
        public EnemyTargeter EnemyTargeter => enemyTargeter;
        public ShamanTargeter ShamanTargeter => shamanTargeter;

        #endregion

        #region Serialized
    
        [SerializeField] private UnitType unitType;
        [SerializeField, TabGroup("Combat")] private UnitAutoCaster _autoCaster;
        [SerializeField, TabGroup("Combat")] private BoxCollider2D boxCollider;
        [SerializeField, TabGroup("Combat")] private GroundCollider groundCollider;
        [SerializeField, TabGroup("Stats")] private UnitStats stats;
        [SerializeField, TabGroup("Movement")] private UnitMovement movement;
        [SerializeField, TabGroup("Visual")] private bool hasHPBar;
        [SerializeField, ShowIf(nameof(hasHPBar)), TabGroup("Visual")] private HP_Bar hpBar;
        [SerializeField, TabGroup("Combat")] private Transform _castPos;
        [SerializeField, TabGroup("Targeter")] private EnemyTargeter enemyTargeter;
        [SerializeField, TabGroup("Targeter")] private ShamanTargeter shamanTargeter;
    
        #endregion
    
    
        public virtual void Init(BaseUnitConfig givenConfig)
        {
            UnitConfig = givenConfig;
            stats = new UnitStats(BaseStats);
            DamageDealer = new DamageDealer(this, givenConfig.AutoAttack);
            Affector = new Affector(this);
            Effectable = new Effectable(this);
            Damageable = new Damageable(this);
            ShamanTargetHelper = new UnitTargetHelper<Shaman.Shaman>(ShamanTargeter, this);
            EnemyTargetHelper = new UnitTargetHelper<Enemy.Enemy>(EnemyTargeter, this);
            UnitTimers = new List<ITimer>();
            Movement.SetUp(this);
            groundCollider.Init(this);
            IsDead = false;
            ToggleCollider(true);
            Damageable.SetRegenerationTimer(stats[StatType.HpRegenInterval].Value);
            if (hasHPBar)
            {
                hpBar.gameObject.SetActive(true);
                hpBar.Init(Damageable.MaxHp, unitType);
                Damageable.OnHealthChange += hpBar.SetBarValue;
            }

            Damageable.OnTakeDamage += LevelManager.Instance.PopupsManager.SpawnDamagePopup;
            Damageable.OnHeal += LevelManager.Instance.PopupsManager.SpawnHealPopup;
            Effectable.OnAffected += LevelManager.Instance.PopupsManager.SpawnStatusEffectPopup;
            Stats[StatType.BaseRange].OnStatChange += EnemyTargeter.AddRadius;
            Stats[StatType.MovementSpeed].OnStatChange += movement.OnSpeedChange;
        
        }
        protected virtual void OnDisable() //unsubscribe to events
        {
            if (ReferenceEquals(LevelManager.Instance, null)) return;
            if (ReferenceEquals(Damageable, null)) return;
            if (ReferenceEquals(Effectable, null)) return;
            Damageable.OnTakeDamage -= LevelManager.Instance.PopupsManager.SpawnDamagePopup;
            Damageable.OnHeal -= LevelManager.Instance.PopupsManager.SpawnHealPopup;
            Effectable.OnAffected -= LevelManager.Instance.PopupsManager.SpawnStatusEffectPopup;
            if (hasHPBar) Damageable.OnHealthChange -= hpBar.SetBarValue;
            Stats[StatType.BaseRange].OnStatChange -= EnemyTargeter.AddRadius;
            Stats[StatType.MovementSpeed].OnStatChange -= movement.OnSpeedChange;
        }
        public void AddStatUpgrades(StatUpgrade[] statUpgrades)
        {
            foreach (var statUpgrade in statUpgrades)
            {
                Stats[statUpgrade.StatType].AddStatValue(statUpgrade.Factor,statUpgrade.StatValue);
            }
        }

        public void ToggleCollider(bool state)
        {
            boxCollider.enabled = state;
        }

        public void OnDeathAnimation()
        {
            IsDead = true;
            Movement.ToggleMovement(false);
            ToggleCollider(false);
            Damageable.ToggleHitable(false);
            _autoCaster.DisableCaster();
        }

        public void ClearUnitTimers()
        {
            foreach (ITimer iTimer in UnitTimers)
            {
                iTimer.RemoveThisTimer();
            }

            UnitTimers.Clear();
        }

        public BaseEntity GameObject => this;

        private void OnValidate()
        {
            boxCollider ??= GetComponent<BoxCollider2D>();
        }
    
    }
}