using Gameplay.Targeter;
using Gameplay.Units.Damage_System;
using Gameplay.Units.Enemy;
using Gameplay.Units.Stats;
using Managers;
using Sound;
using GameTime;
using UnityEngine;
using Visual.HpBar;

namespace Gameplay.CoreTemple
{
    public class CoreTemple : BaseEntity, IDamagable
    {
        public Damageable Damageable => _damageable;

        [SerializeField] private Stats _statsConfig;
        [SerializeField] private GroundColliderTargeter enemyGroundCollider;
        [SerializeField] private HP_Bar hpBar;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform destroyedCore;
        [SerializeField] private SpriteRenderer coreSpriteRenderer;
        [SerializeField] private ParticleSystem glowParticleSystem;
        private UnitStats _stats;
        [SerializeField] private float destroyAnimationsDelay;
    
        private Effectable _effectable;
        private Affector _affector;
        private Damageable _damageable;

        public void Init()
        {
            _stats = new UnitStats(_statsConfig);
            _affector = new Affector(this);
            _effectable = new Effectable(this);
            _damageable = new Damageable(this); 
            _damageable.Init();
            enemyGroundCollider.OnTargetAdded += OnEnemyEnter;
            hpBar.Init(_damageable.MaxHp,UnitType.Temple);
            //ScreenCracksHandler.Instance.InitByCore(this);
            _damageable.OnDeathGFX += OnCoreDeath;
            _damageable.OnTakeFlatDamage += OnGetHit;
            _damageable.OnHeal += Heal;
        }

        private void OnGetHit(Damageable damageable, int damage)
        {
            SoundManager.PlayAudioClip(SoundEffectType.CoreGetHit);
            //ScreenCracksHandler.Instance.StartCracksAnimation(damage);
            hpBar.SetBar(_damageable);
        
            if (_damageable.CurrentHp <= _damageable.MaxHp * 0.33)
            {
                animator.SetBool("L_Crack",true);   
            }
            else if (_damageable.CurrentHp  <= _damageable.MaxHp * 0.66)
            {
                animator.SetBool("R_Crack",true);
            }
        }

        private void OnEnemyEnter(GroundCollider collider)
        {
            collider.Unit.Damageable.TakeFlatDamage(collider.Unit.Damageable.CurrentHp);
            _damageable.TakeFlatDamage(((Enemy)collider.Unit).CoreDamage);
        }

        private void OnCoreDeath()
        {
            BgMusicManager.Instance.StopMusic();
            SoundManager.PlayAudioClip(SoundEffectType.CoreDestroyed);
            destroyedCore.gameObject.SetActive(true);
            coreSpriteRenderer.enabled = false;
            glowParticleSystem.Play();
            GAME_TIME.Pause();
            var camera = GameManager.CameraHandler;
            camera.SetCameraZoom(0);
            camera.SetCameraPosition(transform.position,true);
            Invoke(nameof(DestroyCoreAnimation),destroyAnimationsDelay);
        }

        public void Heal(Damageable damageable,int amount)
        {
            hpBar.SetBar(_damageable);
            //ScreenCracksHandler.Instance.SetStartValue();
        }

        private void DestroyCoreAnimation()
        {
            animator.SetBool("Break",true);
            Invoke(nameof(EndGameScreen),destroyAnimationsDelay);
        }

        private void EndGameScreen() => LevelManager.Instance.EndLevel(false);
        public UnitStats Stats => _stats;
        public Effectable Effectable => _effectable;
        public Affector Affector => _affector;
        public void ClearUnitTimers()
        {
        
        }

        public BaseEntity GameObject => this;
    }
}
