using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Resources;
using System.Reflection;

namespace TestSharp
{
    class Program
    {
        public static readonly int MAX_FILE_SIZE = 2 * 1024 * 1024;
        public static readonly int MIN_N = 10;
        public static readonly int MAX_N = 100000;

        public static void PrintHelpStr()
        {
            Console.WriteLine(String.Format(TestSharp.Properties.Resources.HelpStr));
            Console.WriteLine(String.Format(TestSharp.Properties.Resources.HelpStrDescription1));
            Console.WriteLine(String.Format(TestSharp.Properties.Resources.HelpStrDescription2));
            Console.WriteLine(String.Format(TestSharp.Properties.Resources.HelpStrDescription3));
        }


        /// <summary>
        /// Возвращает true, если аргументы коммандной строки прошли валидацию.
        /// Файлы словаря и текста существуют, доступны для чтения и имеют размер не больш 2 mb.
        /// Число N- оптимальное число строк в генерируемом html-файле лежит в интервале [10..100000].
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
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
                        PrintHelpStr();
                    }
                    break;
                case 3:
                    int cntError = 0;

                    if (!File.Exists(args[0]))
                    {
                        Console.WriteLine(String.Format(TestSharp.Properties.Resources.FileNotExist,args[0]));
                        cntError++;
                    }
                    else
                    {
                        FileInfo someFileInfo = new FileInfo(args[0]);
                        long fileSize = someFileInfo.Length;
                        if (fileSize>MAX_FILE_SIZE)
                        {
                            Console.WriteLine(String.Format(TestSharp.Properties.Resources.MaxFileSizeError,
                                              args[0], MAX_FILE_SIZE/1024*1024));
                            cntError++;
                        }

                         Stream s = new FileStream(args[0],FileMode.Open, FileAccess.Read);
                         bool canRead = s.CanRead;
                         s.Dispose();

                        if (!canRead)
                        {
                             Console.WriteLine(String.Format(TestSharp.Properties.Resources.FileNotCanRead,args[0]));
                             cntError++;
                        }
                    }

                    if (!File.Exists(args[1]))
                    {
                        Console.WriteLine(String.Format(TestSharp.Properties.Resources.FileNotExist, args[1]));
                        cntError++;
                    }
                    else
                    {
                        FileInfo someFileInfo = new FileInfo(args[1]);
                        long fileSize = someFileInfo.Length;
                        if (fileSize>MAX_FILE_SIZE)
                        {
                            Console.WriteLine(String.Format(TestSharp.Properties.Resources.MaxFileSizeError, 
                                              MAX_FILE_SIZE / 1024 * 1024));
                            cntError++;
                        }

                         Stream s = new FileStream(args[1],FileMode.Open, FileAccess.Read);
                         bool canRead = s.CanRead;
                         s.Dispose();

                        if (!canRead)
                        {
                            Console.WriteLine(String.Format(TestSharp.Properties.Resources.FileNotCanRead, args[1]));
                             cntError++;
                        }
                    }

                    int digit =0;
                    bool isDigit = int.TryParse(args[2], out digit);
                    if (!isDigit)
                    {
                        Console.WriteLine(String.Format(TestSharp.Properties.Resources.NotDigitError, args[2]));
                        cntError++;
                    }
                    else
                    {
                        if (digit<MIN_N||digit>MAX_N)
                            Console.WriteLine(String.Format(TestSharp.Properties.Resources.SizeDigitError, args[2], MIN_N, MAX_N));
                    }

                    if (cntError > 0)
                    {
                        PrintHelpStr();
                    }
                    res = (cntError == 0);
                    break;
                default:
                    Console.WriteLine(String.Format(TestSharp.Properties.Resources.InvalidArgsCountError));
                    PrintHelpStr();
                    break;
            }
            return res;
        }
        
        static void Main(string[] args)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            if (!isArgsValid(args))
            {
                Console.ReadKey();
                return;
            }

            Dictionary dict = new Dictionary(args[0], Encoding.UTF8);
            
            HtmlFileManager manager = new HtmlFileManager(new SentenceParser (new WordParser(args[1],Encoding.UTF8)),int.Parse(args[2]),dict);
            while (!manager.EndOfStream)
            {
                manager.SaveNextHtmlFile();
            }
            manager.Dispose();

            sw.Stop();
            Console.WriteLine("затрачено времени:{0}", sw.Elapsed);
            Console.ReadKey();

        }
    }
}
