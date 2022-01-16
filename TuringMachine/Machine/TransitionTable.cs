using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TuringMachineApp.Machine;

namespace TuringMachineApp.Machine
{
    internal class TransitionTable : Dictionary<TransitionKey, Transition>
    {
        private IEnumerable<string> transitionLines;
        private readonly char initialState;
        private readonly char[] acceptingStates;

        public TransitionTable(IEnumerable<string> transitionLines, char initialState, char[] acceptingStates)
        {
            this.transitionLines = transitionLines;
            this.initialState = initialState;
            this.acceptingStates = acceptingStates;
            foreach (var line in transitionLines)
            {
                var transition = new Transition(line);
                this.Add(new TransitionKey(transition.CurrentState, transition.CurrentLetter), transition);
            }
        }
        public override string ToString()
        {
            var builder = new StringBuilder();
            var rows = this.Keys.GroupBy(key => key.State);

            builder.Append(BuildHeaderString(rows));

            foreach (var row in rows)
            {
                var state = row.Key;
                builder.Append(string.Format("{0,2} {1,1} {2,1} |", IsinitialState(state), IsFinalState(state), state));
                foreach (var transition in row)
                {
                    var relation = this[transition];
                    builder.Append(string.Format(" ({0,1},{1,1},{2,1}) |",
                        relation.StateToGo, relation.LetterToWrite, relation.MachineHeadDirection));
                }
                builder.Append("\n");
            }


            return builder.ToString();
        }

        private string BuildHeaderString(IEnumerable<IGrouping<char, TransitionKey>> rows)
        {
            var builder = new StringBuilder();
            builder.Append(string.Format("{0,2} {1,1} {2,1} |", "", "", ""));
            foreach (var row in rows)
            {
                builder.Append(string.Format(" {0,7} |", row.Key));
            }
            builder.Append("\n");
            builder.Append(string.Format("------ |", "", "", ""));
            foreach (var row in rows)
            {
                builder.Append("----------");
            }
            builder.Append("\n");
            return builder.ToString();
        }

        private string IsFinalState(char state)
        {
            return this.acceptingStates.Contains(state) ? "F" : "";
        }

        private string IsinitialState(char state)
        {
            return this.initialState == state ? "->" : "";
        }
    }
}