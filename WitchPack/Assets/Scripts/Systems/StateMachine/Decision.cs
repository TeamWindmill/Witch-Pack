using UnityEngine;

namespace Systems.StateMachine
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(BaseStateMachine machine);
    }
}