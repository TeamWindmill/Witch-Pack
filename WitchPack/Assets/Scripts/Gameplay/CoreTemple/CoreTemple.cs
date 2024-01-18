using System;
using UnityEngine;

public class CoreTemple : MonoBehaviour
{
    public Action OnCoreDestroyed;
    public Action<int> OnGetHit;
    
    [SerializeField] private int maxHp;
    [SerializeField] private EnemyTargeter enemyTargeter;
    [SerializeField] private HP_Bar hpBar;
    
    private int curHp;

    public int MaxHp { get => maxHp; }
    public int CurHp { get => curHp; }

    public void Init()
    {
        curHp = maxHp;
        enemyTargeter.OnTargetAdded += OnEnemyEnter;
        hpBar.Init(maxHp,UnitType.Temple);
        OnGetHit += hpBar.SetBarValue;
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
        if (curHp <= 0)
        {
            OnCoreDestroyed?.Invoke();//lose game? 
            // destroy anim
            LevelManager.Instance.EndLevel(false);
        }
    }

}
