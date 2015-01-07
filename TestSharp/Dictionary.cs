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

        public Dictionary(String fileName)
        {
            set = loadFile(fileName);
        }

        private HashSet<String> loadFile(String fileName)
        {
            HashSet<String> tmpSet = new HashSet<String>();
            StreamReader sr = new StreamReader(fileName,Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                tmpSet.Add(sr.ReadLine().ToUpper());
            }
            sr.Close();
            return tmpSet;
        }


        public bool Contains(String word)
        {
            return set.Contains(word.ToUpper());
        }

    }
}
