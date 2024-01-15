using System;
using UnityEngine;

public class CoreTemple : MonoBehaviour
{
    [SerializeField] private int maxHp;
    private int curHp;
    public Action OnCoreDestroyed;
    public Action<int> OnGetHit;

    public int MaxHp { get => maxHp; }
    public int CurHp { get => curHp; }

    private void Start()
    {
        curHp = maxHp;
    }

    public void TakeDamage(int amount)
    {
        curHp -= amount;
        OnGetHit?.Invoke(amount);//lose game? 
        if (curHp <= 0)
        {
            OnCoreDestroyed?.Invoke();//lose game? 
        }
    }

}
