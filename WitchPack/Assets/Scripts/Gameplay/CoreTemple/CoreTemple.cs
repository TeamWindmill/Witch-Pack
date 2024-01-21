using System;
using UnityEngine;

public class CoreTemple : MonoBehaviour
{
    public Action OnCoreDestroyed;
    public Action<int> OnGetHit;
    
    [SerializeField] private int maxHp;
    [SerializeField] private EnemyTargeter enemyTargeter;
    [SerializeField] private HP_Bar hpBar;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform destroyedCore;
    [SerializeField] private SpriteRenderer coreSpriteRenderer;
    [SerializeField] private ParticleSystem glowParticleSystem;
    [SerializeField] private float destroyAnimationsDelay;
    
    private int curHp;

    public int MaxHp { get => maxHp; }
    public int CurHp { get => curHp; }

    public void Init()
    {
        curHp = maxHp;
        enemyTargeter.OnTargetAdded += OnEnemyEnter;
        hpBar.Init(maxHp,UnitType.Temple);
    }
    private void OnEnemyEnter(Enemy enemy)
    {
        enemy.Damageable.TakeFlatDamage(enemy.Damageable.CurrentHp);
        TakeDamage(enemy.CoreDamage);
    }

    public void TakeDamage(int amount)
    {
        curHp -= amount;
        OnGetHit?.Invoke(amount);
        hpBar.SetBarValue(curHp);
        if (curHp <= 0)
        {
            OnCoreDestroyed?.Invoke();
            // destroy anim
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

    private void DestroyCoreAnimation()
    {
        animator.SetBool("Break",true);
        Invoke(nameof(EndGameScreen),destroyAnimationsDelay);
    }

    private void EndGameScreen() => LevelManager.Instance.EndLevel(false);
}
