using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace TestSharp
{
    class WordParser
    {
        private String fileName;
        private String Delemiters = " :;\"(),+-=<>?!@#$%^&*~'./\\";
        private List<String> arrayWords;

        public WordParser(String fileName, Encoding encoding)
        {
            arrayWords = this.Parse(fileName, encoding);
        }


        public List<String> getWords()
        {
            return arrayWords;
        }

        private List<String> Parse(String fileName, Encoding encoding)
        {
            StreamReader sr = new StreamReader(fileName, encoding);

            List<String> list = new List<string>();

            while (!sr.EndOfStream)
            {
                String temp = sr.ReadLine();
                for (int i = 0; i < temp.Length; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    if (Delemiters.IndexOf(temp[i]) < 0)
                    {
                        sb.Append(temp[i]);
                    }
                    else
                    {
                        list.Add(sb.ToString());
                        list.Add(temp[i].ToString());
                    }
                }
                list.Add("\n");
            }
            list.Add("\n");
            return list;
        }
    }
}
