using System;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.StateMachine
{
    public class BaseStateMachine : MonoBehaviour
    {
        public BaseState CurrentState;
        public Enemy Owner { get; private set; } 
        
        [SerializeField] private BaseState _initialState;
        private bool _initialized;
        private float _executionIntervalTimer;
        private float _transitionIntervalTimer;
        

        public void Init(Enemy owner)
        {
            Owner = owner;
            CurrentState = _initialState;
            _initialized = true;
        }

        private void Update()
        {
            if(!_initialized) return;
            if (_executionIntervalTimer >= CurrentState.ExecuteInterval)
            {
                CurrentState.Execute(this);
                _executionIntervalTimer = 0;
            }
            else
            {
                _executionIntervalTimer += GAME_TIME.GameDeltaTime;
            }
            if (_transitionIntervalTimer >= CurrentState.TransitionInterval)
            {
                CurrentState.Transition(this);
                _transitionIntervalTimer = 0;
            }
            else
            {
                _transitionIntervalTimer += GAME_TIME.GameDeltaTime;
            }
        }

        private void OnDisable()
        {
            _initialized = false;
        }
    }
}