using System;
using System.Collections.Generic;

namespace TuringMachineApp.Machine
{
    internal class TransitionTable : Dictionary<Tuple<char, char>, Transition>
    {
        private IEnumerable<string> transitionLines;

        public TransitionTable(IEnumerable<string> transitionLines)
        {
            this.transitionLines = transitionLines;
            foreach(var line in transitionLines)
            {
                var transition = new Transition(line);
                this.Add(new Tuple<char, char>(transition.CurrentState, transition.CurrentLetter), transition);
            }
        }
    }
}