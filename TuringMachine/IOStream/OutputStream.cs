using System;

namespace TuringMachineApp.IOStream
{
    public class OutputStream
    {
        public OutputStream(int compStatus, string stWord, string endWord, string compLeng)
        {
            calcDetails(compStatus, stWord, endWord, compLeng);
            saveToTxt();
        }

        public void saveToTxt()
        {
            if (outputText() == 1);
            generateOutputFile();
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

        public void generateOutputFile()
        {
        }

        public void calcDetails(int compStatus, string stWord, string endWord, string compLeng)
        {
            if (compStatus == 1) //ok
            {
                Console.WriteLine("\n Słowo obliczone: {0}", endWord);
                Console.WriteLine("\n Słowo początkowe: {0}", stWord);
                Console.WriteLine("\n Długość obliczenia: {0}", compLeng);
            }
            else if (compStatus == 2) //pętla nieskończona
            {
                Console.WriteLine("\n Program wpadł w pętle nieskończoną! \n");
            }
            else//błąd
            {
                Console.WriteLine("\n Błąd w obliczeniach");
            }
        }

    }
}