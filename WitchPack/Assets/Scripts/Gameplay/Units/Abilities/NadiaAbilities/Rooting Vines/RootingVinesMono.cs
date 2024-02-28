using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootingVinesMono : MonoBehaviour
{
    [SerializeField] private float lastingTime;
    [SerializeField] private float elapsedTime;
    [SerializeField] private GroundColliderTargeter groundColliderTargeter;

    protected BaseAbility refAbility;
    protected BaseUnit owner;
    
    

    private void Update()
    {
        elapsedTime += GAME_TIME.GameDeltaTime;
        if(elapsedTime >= lastingTime)
        {
            elapsedTime = 0;
            gameObject.SetActive(false);
            groundColliderTargeter.OnTargetAdded -= OnTargetEntered;
        }
    }

    public virtual void Init(BaseUnit owner, BaseAbility ability)
    {
        this.owner = owner;
        this.refAbility = ability;
        groundColliderTargeter.OnTargetAdded += OnTargetEntered;
    }

    protected virtual void OnRoot(Enemy enemy)
    {

    }

    protected virtual void OnTargetEntered(GroundCollider collider)
    {
        if(collider.Unit is Enemy enemy)
        {
            if (!ReferenceEquals(enemy, null))
            {
                enemy.Damageable.GetHit(owner.DamageDealer, refAbility);
                OnRoot(enemy);
            }
        }
    }
}
