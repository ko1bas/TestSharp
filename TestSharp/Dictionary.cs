using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace TestSharp
{
    class Dictionary 
    {
        private HashSet<String> set;

        public Dictionary(String fileName, Encoding encoding )
        {
            set = loadFile(fileName, encoding);
        }

        private HashSet<String> loadFile(String fileName, Encoding encoding)
        {
            HashSet<String> tmpSet = new HashSet<String>();
            StreamReader sr = new StreamReader(fileName, encoding);
            try
            {
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


        public bool Contains(String word)
        {
            return set.Contains(word.ToUpper());
        }

    }
}
