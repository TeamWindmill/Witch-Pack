using UnityEngine;

namespace Systems.StateMachine
{
    public class BaseState : ScriptableObject
    {
        public float ExecuteInterval;
        public float TransitionInterval;
        public virtual void Execute(BaseStateMachine machine) { }
        public virtual void Transition(BaseStateMachine machine) { }
    }
}