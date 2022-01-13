using System;
using System.Collections.Generic;
using TuringMachineApp.Machine;

namespace TuringMachineApp.Machine
{
    internal class TransitionTable : Dictionary<TransitionKey, Transition>
    {
        private IEnumerable<string> transitionLines;

        public TransitionTable(IEnumerable<string> transitionLines)
        {
            this.transitionLines = transitionLines;
            foreach(var line in transitionLines)
            {
                var transition = new Transition(line);
                this.Add(new TransitionKey(transition.CurrentState, transition.CurrentLetter), transition);
            }
        }
    }
}