﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestSharp
{
    class Program
    {
        public static readonly int MAX_FILE_SIZE = 2 * 1024 * 1024;
        public static readonly int MIN_N = 10;
        public static readonly int MAX_N = 100000;

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
                case 3:
                    int cntError = 0;

                    if (!File.Exists(args[0]))
                    {
                        Console.WriteLine(String.Format("Файл {0} не существует.",args[0]));
                        cntError++;
                    }
                    else
                    {
                        FileInfo someFileInfo = new FileInfo(args[0]);
                        long fileSize = someFileInfo.Length;
                        if (fileSize>MAX_FILE_SIZE)
                        {
                            Console.WriteLine(String.Format("Файл {0} больше {1} mb.",args[0], MAX_FILE_SIZE/1024*1024));
                            cntError++;
                        }

                         Stream s = new FileStream(args[0],FileMode.Open, FileAccess.Read);
                         bool canRead = s.CanRead;
                         s.Dispose();

                        if (!canRead)
                        {
                             Console.WriteLine(String.Format("Не хватает прав доступа чтобы прочитать файл {0}.",args[0]));
                             cntError++;
                        }
                    }

                    if (!File.Exists(args[1]))
                    {
                        Console.WriteLine(String.Format("Файл {0} не существует.",args[1]));
                        cntError++;
                    }
                    else
                    {
                        FileInfo someFileInfo = new FileInfo(args[1]);
                        long fileSize = someFileInfo.Length;
                        if (fileSize>MAX_FILE_SIZE)
                        {
                            Console.WriteLine(String.Format("Файл {0} больше {1} mb.",args[1], MAX_FILE_SIZE/1024*1024));
                            cntError++;
                        }

                         Stream s = new FileStream(args[1],FileMode.Open, FileAccess.Read);
                         bool canRead = s.CanRead;
                         s.Dispose();

                        if (!canRead)
                        {
                             Console.WriteLine(String.Format("не хватает прав доступа чтобы прочитать файл {0}.",args[1]));
                             cntError++;
                        }
                    }

                    int digit =0;
                    bool isDigit = int.TryParse(args[2], out digit);
                    if (!isDigit)
                    {
                        Console.WriteLine(String.Format("{0} не является числом.",args[2]));
                        cntError++;
                    }
                    else
                    {
                        if (digit<MIN_N||digit>MAX_N)
                            Console.WriteLine(String.Format("{0} не  лежит в интервале [{1}..{2}].", args[2], MIN_N, MAX_N));
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

            Dictionary dict = new Dictionary(args[0], Encoding.UTF8);
            
            HtmlFileManager manager = new HtmlFileManager(new SentenceParser (new WordParser(args[1],Encoding.UTF8)),100,dict);
            while (!manager.EndOfStream)
            {
                manager.SaveNextHtmlFile();
            }
            manager.Dispose();
        }
    }
}
