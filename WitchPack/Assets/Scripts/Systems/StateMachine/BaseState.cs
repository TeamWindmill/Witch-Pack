using UnityEngine;

namespace Systems.StateMachine
{
    public class BaseState : ScriptableObject
    {
        public float StateCheckInterval;
        public virtual void Execute(BaseStateMachine machine) { }
    }
}