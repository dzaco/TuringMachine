using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachineApp.Machine
{
    public class TuringMachine
    {
        public TuringMachine(string filePath)
        {
            LoadFromFile(filePath);
        }

        public char[] TapeAlphabet { get; private set; }
        public char[] InputAlphabet { get; private set; }
        public string InputWord { get; private set; }
        public string[] States { get; private set; }
        public string InitialState { get; private set; }
        public string[] AcceptingStates { get; private set; }
        internal TransitionTable Transitions { get; private set; }

        private void LoadFromFile(string filePath)
        {
            var lines = File.ReadLines(filePath)
                .Select(line => line.Replace(":", "").Trim());
                
            for (int i = 0; i < lines.Count(); i++)
            {
                switch (lines.ElementAt(i))
                {
                    case FileManager.ExpectedLine.TapeAlphabet:
                        {
                            var alphabet = lines.ElementAt(++i);
                            this.TapeAlphabet = alphabet.ToCharArray();
                            break;
                        }
                    case FileManager.ExpectedLine.InputAlphabet:
                        {
                            var alphabet = lines.ElementAt(++i);
                            this.InputAlphabet = alphabet.ToCharArray();
                            break;
                        }
                    case FileManager.ExpectedLine.InputWord:
                        {
                            this.InputWord = lines.ElementAt(++i);
                            break;
                        }
                    case FileManager.ExpectedLine.States:
                        {
                            var states = lines.ElementAt(++i);
                            this.States = states.Split(' ');
                            break;
                        }
                    case FileManager.ExpectedLine.InitialState:
                        {
                            this.InitialState = lines.ElementAt(++i);
                            break;
                        }
                    case FileManager.ExpectedLine.AcceptingStates:
                        {
                            var states = lines.ElementAt(++i);
                            this.AcceptingStates = states.Split(' ');
                            break;
                        }
                    case FileManager.ExpectedLine.TransitionRelationship:
                        {
                            var transitionLines = lines.Skip(i + 1);
                            this.Transitions = new TransitionTable(transitionLines);
                            return; // transition read to end of file
                        }

                    default:
                        break;
                }
            }
        }
    }
}
