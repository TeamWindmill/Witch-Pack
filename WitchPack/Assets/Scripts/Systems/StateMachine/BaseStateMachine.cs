using System;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.StateMachine
{
    public class BaseStateMachine : MonoBehaviour
    {
        public BaseState CurrentState { get; set; }
        public Enemy Owner { get; private set; } 
        
        [SerializeField] private BaseState _initialState;
        private bool _initialized;
        private float _intervalTimer;
        

        public void Init(Enemy owner)
        {
            Owner = owner;
            CurrentState = _initialState;
            _initialized = true;
        }

        private void Update()
        {
            if(!_initialized) return;
            if (_intervalTimer < CurrentState.StateCheckInterval)
            {
                CurrentState.Execute(this);
                _intervalTimer = 0;
            }
            else
            {
                _intervalTimer += GAME_TIME.GameDeltaTime;
            }
        }

        private void OnDisable()
        {
            _initialized = false;
        }
    }
}