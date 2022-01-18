using System;
using System.IO;
using System.Text.RegularExpressions;

namespace TuringMachineApp.IOStream
{
    public class OutputStream
    {
        public OutputStream(string toFile, int compStatus, string stWord, string endWord, int compLeng)
        {
            calcDetails(compStatus, stWord, endWord, compLeng);
            saveToTxt(toFile);
        }

        public void saveToTxt(string toFile)
        {
            if (outputText() == 1)
            {
                generateOutputFile(toFile);
            }
        }

        public int outputText()
        {
            string text = @" 
Czy chcesz zapisac przebieg konfiguracji do pliku out.txt ? 
1 - Potwierdz
0 - Odrzuc 
";
            Console.WriteLine(text);
            string userOption = Console.ReadLine();
            
            if (Convert.ToInt32(userOption) == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
                
        }

        public void generateOutputFile(string toFile)
        {
            string pattern = "[" + Regex.Escape("\n") + Regex.Escape(".") + "]";
            string[] txtArray = Regex.Split(toFile, pattern);
            string fileName = "out.txt";
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,fileName);
            File.WriteAllLines(destPath, txtArray);
        }

        public void calcDetails(int compStatus, string stWord, string endWord, int compLeng)
        {
            if (compStatus == 1) //ok
            {
                Console.WriteLine("\nSłowo obliczone: {0}", endWord);
                Console.WriteLine("\nSłowo początkowe: {0}", stWord);
                Console.WriteLine("\nDługość obliczenia: {0}", compLeng);
            }
            else if (compStatus == 2) //pętla nieskończona
            {
                Console.WriteLine("\nProgram wpadł w pętle nieskończoną! \n");
            }
            else//błąd
            {
                Console.WriteLine("\nBłąd w obliczeniach");
            }
        }
    }
}