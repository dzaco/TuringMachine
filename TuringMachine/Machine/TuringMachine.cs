using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TuringMachineApp.Machine;

namespace TuringMachineApp.Machine
{
    public class TuringMachine
    {
        public TuringMachine(string filePath)
        {
            LoadFromFile(filePath);
            Tape = new Tape(TapeAlphabet, InputWord);
        }

        public char[] TapeAlphabet { get; private set; }
        public char[] InputAlphabet { get; private set; }
        public string InputWord { get; private set; }
        public char[] States { get; private set; }
        public char InitialState { get; private set; }
        public char[] AcceptingStates { get; private set; }
        internal TransitionTable Transitions { get; private set; }
        public Tape Tape { get; }
        public char CurrentState { get; private set; }

        public void LoadFromFile(string filePath)
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
                            this.States = states.Where(state => !Char.IsWhiteSpace(state)).ToArray();
                            break;
                        }
                    case FileManager.ExpectedLine.InitialState:
                        {
                            this.InitialState = lines.ElementAt(++i).First();
                            this.CurrentState = InitialState;
                            break;
                        }
                    case FileManager.ExpectedLine.AcceptingStates:
                        {
                            var states = lines.ElementAt(++i);
                            this.AcceptingStates = states.Where(state => !Char.IsWhiteSpace(state)).ToArray();
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

        public int Start()
        {
            Console.WriteLine("Initial Tape");
            Console.WriteLine(Tape);
            var iteration = 0;
            while(true)
            {
                //Thread.Sleep(250);
                if (Transitions.TryGetValue(new TransitionKey(CurrentState, Tape.Head.Value), out var transition))
                {
                    CurrentState = transition.StateToGo;
                    Tape.Head.Value = transition.LetterToWrite;
                    Tape.Move(transition.MachineHeadDirection);
                    Console.WriteLine(CurrentState);
                    Console.WriteLine(Tape);
                    if (this.AcceptingStates.Contains(CurrentState))
                    {
                        Console.WriteLine($"Program stoped in accepting state \'{CurrentState}\'");
                        break;
                    }
                }
                else
                    throw new InvalidOperationException("No transation for current configuration. Program Failed.");

                iteration++;
            }
            
            return iteration;
        }
    }
}
