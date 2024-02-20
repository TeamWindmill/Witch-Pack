using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootingVinesMono : MonoBehaviour
{
    [SerializeField] private float lastingTime;
    [SerializeField] private float elapsedTime;

    protected BaseAbility refAbility;
    protected BaseUnit owner;
    
    

    private void Update()
    {
        elapsedTime += GAME_TIME.GameDeltaTime;
        if(elapsedTime >= lastingTime)
        {
            elapsedTime = 0;
            gameObject.SetActive(false);
        }
    }

    public virtual void Init(BaseUnit owner, BaseAbility ability)
    {
        this.owner = owner;
        this.refAbility = ability;
    }

    protected virtual void OnRoot(Enemy enemy)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (!ReferenceEquals(enemy, null))
        {
            enemy.Damageable.GetHit(owner.DamageDealer, refAbility);
            OnRoot(enemy);
            
        }
    }
}
