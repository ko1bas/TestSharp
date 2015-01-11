using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;

namespace TestSharp
{
    /// <summary>
    /// Хранит список словарных слов.
    /// </summary>
    class Dictionary 
    {
        private HashSet<String> set;

        public Dictionary(String fileName, Encoding encoding )
        {
            set = loadFile(fileName, encoding);
        }

        /// <summary>
        /// Загружает файл словаря в указанной кодировке.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        private HashSet<String> loadFile(String fileName, Encoding encoding)
        {
            HashSet<String> tmpSet = new HashSet<String>();
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(fileName, encoding);
                while (!sr.EndOfStream)
                {
                    tmpSet.Add(sr.ReadLine().ToUpper());
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            finally
            {
                if (sr!=null)
                    sr.Dispose();
            }
            return tmpSet;
        }

        /// <summary>
        /// Возвращает true, если слово присутствует в словаре.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool Contains(String word)
        {
            return set.Contains(word.ToUpper());
        }

    }
}
