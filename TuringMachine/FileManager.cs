using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachineApp
{
    public static class FileManager
    {
        public static string ProjectPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
        public static string GetFirstFile()
        {
            return Directory.GetFiles(ProjectPath)
                .Where(file => Path.GetExtension(file) == ".txt")
                .FirstOrDefault();
        }

        // not working in console app
        //public static string GetLoadPathFromDialog()
        //{
        //    var dialog = new OpenFileDialog();
        //    dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //    dialog.DefaultExt = ".txt";
        //    dialog.Filter = "txt files (*.txt)|*.txt";

        //    if (dialog.ShowDialog() == true)
        //        return dialog.FileName;
        //    else
        //        return null;
        //}
        public static class ExpectedLine
        {
            public const string TapeAlphabet = "alfabet tasmowy";
            public const string InputAlphabet = "alfabet wejsciowy";
            public const string InputWord = "slowo wejsciowe";
            public const string States = "stany";
            public const string InitialState = "stan poczatkowy";
            public const string AcceptingStates = "stany akceptujace";
            public const string TransitionRelationship = "relacja przejscia";
        }
    }
}
