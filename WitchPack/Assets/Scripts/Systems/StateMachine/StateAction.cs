using UnityEngine;

namespace Systems.StateMachine
{
    public abstract class StateAction : ScriptableObject
    {
        public abstract void Execute(BaseStateMachine machine);
    }
}