using System;
using System.Linq;

namespace TuringMachineApp.Machine
{
    internal class Transition
    {

        public Transition(string line)
        {
            sourceLine = line;
            var param = line.Where(c => !Char.IsWhiteSpace(c)).ToArray();
            CurrentState = param[0];
            CurrentLetter = param[1];
            StateToGo = param[2];
            LetterToWrite = param[3];
            MachineHeadDirection = param[4];
        }

        private string sourceLine;
        public char CurrentState { get; }
        public char CurrentLetter { get; }
        public char StateToGo { get; }
        public char LetterToWrite { get; }
        public char MachineHeadDirection { get; }
    }
}