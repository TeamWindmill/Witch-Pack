using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Systems.StateMachine
{
    public abstract class BaseStateMachine<T> : MonoBehaviour where T : MonoBehaviour
    {
        public State<T> ActiveState => _activeState;
        
        [ShowInInspector] protected State<T> _activeState;
        
        private Dictionary<Type, State<T>> _stateByType = new ();
        private bool _isActive;
        
        protected void BaseInit(List<State<T>> states)
        {
            _stateByType = new ();
            foreach (var state in states)
            {
                _stateByType.Add(state.GetType(), state);
            }

            _activeState = states[0];
            _isActive = true;
        }

        public void SetState(Type newStateType)
        {
            if (_activeState != null)
            {
                _activeState.Exit(this as T);
            }

            _activeState = _stateByType[newStateType];
            _activeState.Enter(this as T);
        }

        public void Resume() => _isActive = true;
        public void Stop() => _isActive = false;

        private void Update()
        {
            if (!_isActive) return;
            _activeState.UpdateState(this as T);
            _activeState.ChangeState(this as T);
        }
    }
    
}