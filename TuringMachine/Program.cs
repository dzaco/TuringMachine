using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuringMachineApp.IOStream;
using TuringMachineApp.Machine;

namespace TuringMachineApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var Tpl = new InputStream().StartupConsole();
            var path = FileManager.GetFirstFile();
            var turingMachine = new TuringMachine(path);
            try
            {
                var iteration = turingMachine.Start(Tpl);
                Console.WriteLine($"Complete in {iteration} move");
                var outputData = new OutputStream('0',"0","0","0");
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed");
            }
            Console.ReadKey();
        }
    }
}
