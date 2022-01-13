using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachineApp.Machine
{
    public struct TransitionKey
    {
        public char State;
        public char Letter;

        public TransitionKey(char currentState, char currentLetter) : this()
        {
            State = currentState;
            Letter = currentLetter;
        }

        public override bool Equals(object obj)
        {
            return obj is TransitionKey key &&
                   State == key.State &&
                   Letter == key.Letter;
        }

        public override int GetHashCode()
        {
            int hashCode = -410670205;
            hashCode = hashCode * -1521134295 + State.GetHashCode();
            hashCode = hashCode * -1521134295 + Letter.GetHashCode();
            return hashCode;
        }
    }
}
