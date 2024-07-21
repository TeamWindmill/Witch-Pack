using Sirenix.OdinInspector;
using UnityEngine;

namespace Systems.StateMachine
{
    public abstract class IntervalState<T> : State<T> 
        where T : MonoBehaviour
    {
        [SerializeField,BoxGroup("Interval State")] protected bool _usingGameTime;
        [SerializeField,BoxGroup("Interval State"), Tooltip("seconds between the logical execution of the state")] protected float _executionInterval;
        [SerializeField,BoxGroup("Interval State"), Tooltip("seconds between checking whether to change state")] protected float _stateCheckInterval;
        [ReadOnly,BoxGroup("Interval State")] protected string StateCheckDescription;
        protected float _executionTimer;
        protected float _stateCheckTimer;

        public override void Enter(T parent)
        {
            _executionTimer = 0;
            _stateCheckTimer = 0;
            base.Enter(parent);
        }

        public sealed override void UpdateState(T parent)
        {
            if (_executionTimer >= _executionInterval)
            {
                IntervalUpdate(parent);
                _executionTimer = 0;
            }
            else
            {
                _executionTimer += _usingGameTime ? GAME_TIME.GameDeltaTime : Time.deltaTime;
            }
        }
        protected abstract void IntervalUpdate(T parent);
        public /* sealed */ override void ChangeState(T parent)
        {
            if (_stateCheckTimer >= _stateCheckInterval)
            {
                IntervalChangeState(parent);
                _stateCheckTimer = 0;
            }
            else
            {
                _stateCheckTimer += _usingGameTime ? GAME_TIME.GameDeltaTime : Time.deltaTime;
            }
        }
        protected abstract void IntervalChangeState(T parent);

        
    }
}