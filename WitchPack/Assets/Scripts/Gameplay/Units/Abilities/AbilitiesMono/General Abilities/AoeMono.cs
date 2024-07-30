using Gameplay.Pools.Pool_System;
using Gameplay.Targeter;
using Gameplay.Units.Abilities.AbilitySystem.BaseAbilities;
using GameTime;
using UnityEngine;

namespace Gameplay.Units.Abilities.AbilitiesMono.General_Abilities
{
    public class AoeMono : MonoBehaviour,IPoolable
    {
        private float ringLastingTime;
        private float elapsedTime;
        [SerializeField] private Transform _holder;
        [SerializeField] private Transform _rangeVisuals;
        [SerializeField] private GroundColliderTargeter _groundColliderTargeter;

        protected CastingAbility Ability;
        protected BaseUnit _owner;
        public virtual void Init(BaseUnit owner, CastingAbility ability, float lastingTime,float aoeRange)
        {
            ringLastingTime = lastingTime;
            _owner = owner;
            Ability = ability;
            _holder.transform.localScale = new Vector3(aoeRange,aoeRange,aoeRange);
            _rangeVisuals.transform.localScale = new Vector3(aoeRange,aoeRange,aoeRange);
            _groundColliderTargeter.OnTargetAdded += OnTargetEnter;
            _groundColliderTargeter.OnTargetLost += OnTargetExit;
        }
        protected virtual void Update()
        {
            elapsedTime += GAME_TIME.GameDeltaTime;
            if(elapsedTime >= ringLastingTime)
            {
                elapsedTime = 0;
                gameObject.SetActive(false);
            }
        }

        protected virtual void OnDisable()
        {
            _groundColliderTargeter.OnTargetAdded -= OnTargetEnter;
            _groundColliderTargeter.OnTargetLost -= OnTargetExit;
        }

        private void OnTargetEnter(GroundCollider collider) => TargetInteract(collider, true);
        private void OnTargetExit(GroundCollider collider) => TargetInteract(collider, false);
    


        private void TargetInteract(GroundCollider collider, bool enter)
        {
            if(collider.Unit is Enemy.Enemy enemy)
            {
                if (enter) OnEnemyEnter(enemy);
                else OnEnemyExit(enemy);

            }
            else if (collider.Unit is Shaman shaman)
            {
                if (enter) OnShamanEnter(shaman);
                else OnShamanExit(shaman);
            }
        }

        protected virtual void OnShamanEnter(Shaman shaman)
        {
        
        }
        protected virtual void OnShamanExit(Shaman shaman)
        {
        
        }
        protected virtual void OnEnemyEnter(Enemy.Enemy enemy)
        {
        
        }
        protected virtual void OnEnemyExit(Enemy.Enemy enemy)
        {
        
        }
        public GameObject PoolableGameObject => gameObject;
    }
}