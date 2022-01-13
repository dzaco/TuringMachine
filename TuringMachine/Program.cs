using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringMachineApp.Machine;

namespace TuringMachineApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = FileManager.GetFirstFile();
            var turingMachine = new TuringMachine(path);
        }
    }
}
