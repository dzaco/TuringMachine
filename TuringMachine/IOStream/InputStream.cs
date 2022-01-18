using System;

namespace TuringMachineApp.IOStream
{
    public class InputStream
    {

        public InputStream()
        {
            //StartupConsole();
        }

        
        public Tuple<int, int> StartupConsole()
        {
            string welcomeText = $@"
 _____                _              ___  ___           _     _             
|_   _|              (_)             |  \/  |          | |   (_)            
  | | ___  _   _ _ __ _ _ __   __ _  | .  . | __ _  ___| |__  _ _ __   ___  
  | |/ _ \| | | | '__| | '_ \ / _` | | |\/| |/ _` |/ __| '_ \| | '_ \ / _ \ 
  | | (_) | |_| | |  | | | | | (_| | | |  | | (_| | (__| | | | | | | |  __/ 
  \_/\___/ \__,_|_|  |_|_| |_|\__, | \_|  |_/\__,_|\___|_| |_|_|_| |_|\___| 
                               __/ |                                        
                              |___/                                         
  
Program załaduje automatycznie pierwszy plik .txt w aktyualnym folderze: {FileManager.ProjectPath}

Wybierz tryb w jakim chcesz przeprowadzic symulację 
(wybierz cyfre i potwierdz enterem):
1 - Co jedna konfiguracja 
2 - Co N konfiguracji
3 - Wynik końcowy            
";
            Console.WriteLine(welcomeText);
            string userOption = Console.ReadLine();

            if (Convert.ToInt32(userOption) == 1)
            {
                return new Tuple<int, int>(Convert.ToInt32(userOption), 0);
            }
            else if (Convert.ToInt32(userOption) == 2)
            {
                Console.WriteLine("\n Podaj wartość N: \n");
                String userIn = Console.ReadLine();
                int nVal;
                while(!int.TryParse(userIn, out nVal))
                {
                    Console.WriteLine("Wprowadz poprawną wartość!");
                    userIn = Console.ReadLine();
                }
                return new Tuple<int, int>(Convert.ToInt32(userOption), nVal);
            }
            else if (Convert.ToInt32(userOption) == 3)
            {
                Console.WriteLine("\nPokaż obliczenia? \n");
                Console.WriteLine("1 - Tak \n");
                Console.WriteLine("2 - Nie \n");
                String userIn = Console.ReadLine();
                while(true)
                {
                    if(Convert.ToInt32(userIn) != 1 && Convert.ToInt32(userIn) != 2)
                    {
                        Console.WriteLine("Wprowadz poprawną wartość!");
                        userIn = Console.ReadLine();
                    }
                    else
                    {
                        break;
                    }
                }
                return new Tuple<int, int>(Convert.ToInt32(userOption), Convert.ToInt32(userIn));
            }
            else
            {
                Console.WriteLine("\n Niepoprawnie wybrana opcja!");
                System.Environment.Exit(1);
                return null;
            }
        }
    }
}