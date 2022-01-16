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
                OutputData opd = turingMachine.Start(Tpl);
                Console.WriteLine($"Complete in {opd.ComputationLength} move");
                OutputStream oS = new OutputStream(opd.textToFile, opd.ComputationStatus,opd.StartWord,opd.EndWord,opd.ComputationLength);
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed");
            }
            Console.ReadKey();
        }
    }
}
