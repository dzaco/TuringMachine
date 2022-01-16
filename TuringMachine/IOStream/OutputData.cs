using System.Windows.Controls;

namespace TuringMachineApp.IOStream
{
    public class OutputData
    {
        public OutputData(string sWord, string eWord, int cLeng, int cStatus, string tTF)
        {
            StartWord = sWord;
            EndWord = eWord;
            ComputationLength = cLeng;
            ComputationStatus = cStatus;
            textToFile = tTF;
        }
        public  string StartWord;
        public  string EndWord;
        public int ComputationLength;
        public  int ComputationStatus;
        public string textToFile;
    }
}