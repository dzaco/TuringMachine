using System;
using System.Collections.Generic;
using System.Text;

namespace TuringMachineApp.Machine
{
    public class Tape : LinkedList<Char>
    {
        public Tape(char[] tapeAlphabet, string inputWord)
        {
            TapeAlphabet = tapeAlphabet;
            SourceWord = inputWord;
            Build();
        }


        public char[] TapeAlphabet { get; }
        public string SourceWord { get; }
    
        public string lastSavedWord { get; set; }
        
        public LinkedListNode<Char> Head { get; private set; }

        private void Build()
        {
            foreach(Char c in SourceWord)
            {
                this.AddLast(c);
            }
            this.Head = this.First;
        }
        public LinkedListNode<Char> Move(char direction)
        {
            if (direction == 'P')
                Head = MoveRight();
            else if (direction == 'L')
                Head = MoveLeft();
            else
                throw new ArgumentOutOfRangeException($"Expected direction \'L\' or \'P\' but was {direction}");

            return Head;
        }
        private LinkedListNode<Char> MoveRight()
        {
            if (Head.Next is null)
                Head = this.AddLast('#');
            else
                Head = Head.Next;

            return Head;
        }

        private LinkedListNode<Char> MoveLeft()
        {
            if (Head.Previous is null)
                Head = this.AddFirst('#');
            else
                Head = Head.Previous;

            return Head;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            var cell = this.First;
            var headIndex = 0;
            var index = 0;
            while(cell != null)
            {
                builder.Append($"{cell.Value}|");

                if (cell == Head)
                    headIndex = index;

                cell = cell.Next;
                index++;
            }
            lastSavedWord = "";
            lastSavedWord = builder.ToString();
            builder.Append("\n")
                .Append(' ', headIndex * 2)
                .Append("^.");
            return builder.ToString();
        }
    }
}