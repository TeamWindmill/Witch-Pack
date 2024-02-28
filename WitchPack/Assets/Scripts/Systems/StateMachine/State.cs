using System.Collections.Generic;
using UnityEngine;

namespace Systems.StateMachine
{
    [CreateAssetMenu(menuName = "StateMachine/State", fileName = "State")]
    public class State : BaseState
    {
        public List<StateAction> Actions = new List<StateAction>();
        public List<Transition> Transitions = new List<Transition>();

        public override void Execute(BaseStateMachine machine)
        {
            foreach (var action in Actions)
                action.Execute(machine);

            foreach(var transition in Transitions)
                transition.Execute(machine);
        }
    }
}