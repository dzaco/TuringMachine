using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TuringMachineApp.IOStream;
using TuringMachineApp.Machine;

namespace TuringMachineApp
{
    public class TuringMachine
    {
        public TuringMachine(string filePath)
        {
            LoadFromFile(filePath);
            Tape = new Tape(TapeAlphabet, InputWord);
        }
        public int status { get; set; }
        public char[] TapeAlphabet { get; private set; }
        public char[] InputAlphabet { get; private set; }
        public string InputWord { get; private set; }
        public char[] States { get; private set; }
        public char InitialState { get; private set; }
        public char[] AcceptingStates { get; private set; }
        internal TransitionTable Transitions { get; private set; }
        public Tape Tape { get; }
        public char CurrentState { get; private set; }
        public int MaxIteration { get; set; } = 2000;

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
                            var transitionLines = lines.Skip(i + 1).Where(line => !string.IsNullOrWhiteSpace(line));
                            this.Transitions = new TransitionTable(transitionLines, this.InitialState, this.AcceptingStates);
                            return; // transition read to end of file
                        }

                    default:
                        break;
                }
            }
        }

        public OutputData Start(Tuple<int, int> tpl)
        {
            Console.WriteLine("Initial Tape");
            Console.WriteLine(Tape);
            var iteration = 0;
            string toTxt = "";
            string loopBreaker = "";
            int promptedIterNum = 0;
            
            while(true)
            {
                if (iteration > MaxIteration)
                    throw new IndexOutOfRangeException($"Possibility of an infinite loop. The machine has already run the maximum number of iterations ({MaxIteration}) and has not found a solution.");

                if (Tape.Count % 32 == 0 && iteration > 0 && Tape.Count != promptedIterNum)
                {
                    var msg = $"Limit taśmy został przerkoczony ({Tape.Count}).\n Taśma zostanie rozszerzona do {Tape.Count + 32} komórek.";
                    Console.WriteLine(msg);
                    toTxt += $"{msg} \n";
                    promptedIterNum = Tape.Count;
                }
                //Thread.Sleep(250);
                if (Transitions.TryGetValue(new TransitionKey(CurrentState, Tape.Head.Value), out var transition))
                {
                    if (tpl.Item1 == 1)
                    {
                        CurrentState = transition.StateToGo;
                        Tape.Head.Value = transition.LetterToWrite;
                        Tape.Move(transition.MachineHeadDirection);
                        Console.WriteLine("Current state: {0}", CurrentState);
                        Console.WriteLine(Tape);
                        toTxt += Tape;
                        loopBreaker = Console.ReadLine();
                        if (this.AcceptingStates.Contains(CurrentState))
                        {
                            Console.WriteLine($"Program stoped in accepting state \'{CurrentState}\'");
                            status = 1;
                            break;
                        }
                    }
                    else if (tpl.Item1 == 2)
                    {
                        CurrentState = transition.StateToGo;
                        Tape.Head.Value = transition.LetterToWrite;
                        Tape.Move(transition.MachineHeadDirection);
                        if (iteration % tpl.Item2 == 0)
                        {
                            Console.WriteLine("Current state: {0}", CurrentState);
                            Console.WriteLine(Tape);
                        }
                        toTxt += Tape;
                        if (this.AcceptingStates.Contains(CurrentState))
                        {
                            Console.WriteLine($"Program stoped in accepting state \'{CurrentState}\'");
                            status = 1;
                            break;
                        }
                    }
                    else if (tpl.Item1 == 3)
                    {
                        CurrentState = transition.StateToGo;
                        Tape.Head.Value = transition.LetterToWrite;
                        Tape.Move(transition.MachineHeadDirection);
                        toTxt += Tape;
                        if (tpl.Item2 == 2)
                        {
                            Console.WriteLine("Current state: {0}", CurrentState);
                            Console.WriteLine(Tape);
                        }
                        if (this.AcceptingStates.Contains(CurrentState))
                        {
                            Console.WriteLine($"Program stoped in accepting state \'{CurrentState}\'");
                            status = 1;
                            break;
                        }
                    }
                }
                else
                    throw new InvalidOperationException("No transation for current configuration. Program Failed.");

                iteration++;
            }

            OutputData oData = new OutputData(InputWord,Tape.Word, iteration,1, toTxt);
            return oData;
        }
    }
}
