using System;
using UnityEngine;

public class CoreTemple : MonoBehaviour
{
    public Action OnCoreDestroyed;
    public event Action<int> OnGetHit;
    
    [SerializeField] private int maxHp;
    [SerializeField] private GroundColliderTargeter enemyGroundCollider;
    [SerializeField] private HP_Bar hpBar;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform destroyedCore;
    [SerializeField] private SpriteRenderer coreSpriteRenderer;
    [SerializeField] private ParticleSystem glowParticleSystem;
    [SerializeField] private float destroyAnimationsDelay;
    
    private int curHp;

    public int MaxHp => maxHp;
    public int CurHp => curHp;

    public void Init()
    {
        curHp = maxHp;
        enemyGroundCollider.OnTargetAdded += OnEnemyEnter;
        hpBar.Init(maxHp,UnitType.Temple);
        ScreenCracksHandler.Instance.InitByCore(this);
    }
    private void OnEnemyEnter(GroundCollider collider)
    {
        collider.Unit.Damageable.TakeFlatDamage(collider.Unit.Damageable.CurrentHp);
        TakeDamage((collider.Unit as Enemy).CoreDamage);
    }

    public void TakeDamage(int amount)
    {
        curHp -= amount;
        if(curHp <0)
            curHp = 0;
        OnGetHit?.Invoke(amount);
        SoundManager.Instance.PlayAudioClip(SoundEffectType.CoreGetHit);
        ScreenCracksHandler.Instance.StartCracksAnimation(amount);
        hpBar.SetBarValue(curHp);
        if (curHp <= 0)
        {
            OnCoreDestroyed?.Invoke();
            BgMusicManager.Instance.StopMusic();
            SoundManager.Instance.PlayAudioClip(SoundEffectType.CoreDestroyed);
            destroyedCore.gameObject.SetActive(true);
            coreSpriteRenderer.enabled = false;
            glowParticleSystem.Play();
            GAME_TIME.Pause();
            var camera = GameManager.Instance.CameraHandler;
            camera.SetCameraPosition(transform.position,true);
            camera.SetCameraZoom(0);
            Invoke(nameof(DestroyCoreAnimation),destroyAnimationsDelay);
            
        }
        else if (curHp <= maxHp * 0.33)
        {
         animator.SetBool("L_Crack",true);   
        }
        else if (curHp <= maxHp * 0.66)
        {
            animator.SetBool("R_Crack",true);
        }
        
    }

    public void Heal(int amount)
    {
        curHp += amount;
        if (curHp > maxHp) curHp = maxHp;
        hpBar.SetBarValue(curHp);
        OnGetHit?.Invoke(-amount);
        ScreenCracksHandler.Instance.SetStartValue();
    }

    private void DestroyCoreAnimation()
    {
        animator.SetBool("Break",true);
        Invoke(nameof(EndGameScreen),destroyAnimationsDelay);
    }

    private void EndGameScreen() => LevelManager.Instance.EndLevel(false);
}
