using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace TestSharp
{
    class WordParser : IDisposable
    {
        private List<String> arrayWords;
        private StreamReader sr;

        public void Dispose()
        {
            if (sr != null)
                sr.Dispose();
        }

        public bool EndOfStream
        {
            get
            {
                return (sr==null || sr.EndOfStream);
            }
        }

        public WordParser(String fileName, Encoding encoding)
        {
            try
            {
                sr = new StreamReader(fileName, encoding);
            }
            catch 
            {
                sr = null;
            }
            arrayWords  = new List<string>();
        }

        public List<String> ReadLine()
        {
            arrayWords.Clear();
            if (!this.EndOfStream)
            {
                arrayWords.Clear();
                String temp = sr.ReadLine();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < temp.Length; i++)
                {
                    if (SentenceParser.Delemiters.IndexOf(temp[i]) < 0)
                    {
                        sb.Append(temp[i]);
                    }
                    else
                    {
                        arrayWords.Add(sb.ToString());
                        sb.Clear();
                        arrayWords.Add(temp[i].ToString());
                    }
                }
                arrayWords.Add(Sentence.RF);

            }
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
                    if (SentenceParser.Delemiters.IndexOf(temp[i]) < 0)
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
