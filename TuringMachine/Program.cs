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
            TuringMachine turingMachine = null;
            try
            {
                var Tpl = new InputStream().StartupConsole();
                var path = FileManager.GetFirstFile();
                Console.WriteLine($"Loaded file: {path}");
                turingMachine = new TuringMachine(path);
                Console.WriteLine("Transition Relationship Table:\n");
                Console.WriteLine(turingMachine.Transitions);
          
                OutputData opd = turingMachine.Start(Tpl);
                Console.WriteLine($"Complete in {opd.ComputationLength} move");
                OutputStream oS = new OutputStream(opd.textToFile, opd.ComputationStatus,opd.StartWord,opd.EndWord,opd.ComputationLength);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
                
                if(turingMachine != null && turingMachine.Tape != null)
                {
                    Console.WriteLine("Last word on the Tape:");
                    Console.WriteLine(turingMachine.Tape);
                }
            }
            Console.ReadKey();
        }
    }
}
