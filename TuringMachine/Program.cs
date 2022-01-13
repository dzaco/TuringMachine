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
            try
            {
                var iteration = turingMachine.Start();
                Console.WriteLine($"Complete in {iteration} move");
            }
            catch (Exception)
            {
                Console.WriteLine($"Faild");
            }
            Console.ReadKey();
        }
    }
}
