using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestSharp
{
    class Program
    {

        public static void PrintHelpStr()
        {
            Console.WriteLine(String.Format("Формат команды: testSharp dictFile.txt textFile.txt"));
        }

        public static void ErrorStr()
        {
            Console.WriteLine("Ошибка");
        }

        public static bool isArgsValid(String[] args)
        {
            bool res = false;
            switch (args.Length)
            {
                case 0:
                    PrintHelpStr();
                    break;
                case 1:
                    if (args[0].ToUpper().Equals("-HELP") || args[0].ToUpper().Equals("/?"))
                        PrintHelpStr();
                    else
                    {
                        ErrorStr();
                        PrintHelpStr();
                    }
                    break;
                case 2:
                    int cntError = 0;
                    if (!File.Exists(args[0]))
                    {
                        Console.WriteLine(String.Format("Файл {0} не существует.",args[0]));
                        cntError++;
                    }

                    if (!File.Exists(args[1]))
                    {
                        Console.WriteLine(String.Format("Файл {0} не существует.", args[1]));
                        cntError++;
                    }

                    if (cntError > 0)
                    {
                        ErrorStr();
                        PrintHelpStr();
                    }
                    res = (cntError == 0);
                    break;
                default:
                    ErrorStr();
                    PrintHelpStr();
                    break;
            }
            return res;
        }
        
        static void Main(string[] args)
        {
            if (!isArgsValid(args))
                return;

            Dictionary dict = new Dictionary(args[0]);
            Console.WriteLine(dict.Contains("абонентка"));
            Console.ReadKey();

        }
    }
}
