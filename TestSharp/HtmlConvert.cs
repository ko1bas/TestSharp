using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestSharp
{
    class HtmlConvert
    {
        private List<Sentence> listSentence;
        private Dictionary dict;

        private static readonly String header = "<!DOCTYPE HTML><html><head><meta charset=\"UTF-8\"/><title>My text</title>"
                                                +"<style>.b{type=\"text/css\";font-weight:bold;font-style:italic;}</style></head><body><pre>";
        private static readonly String footer = "</pre></body></html>";
        private static readonly String spanBegin = "<span class=\"b\">";
        private static readonly String spanEnd = "</span>";

        public HtmlConvert(List<Sentence> listSentence, Dictionary dict)
        {
            this.listSentence = listSentence;
            this.dict = dict;
        }

        public void Save(String fileName)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(fileName, false, Encoding.UTF8);
                sw.Write(header);
                StringBuilder sb = new StringBuilder();
                foreach (Sentence sentence in listSentence)
                {
                    sb.Clear();
                    foreach (Word word in sentence.Words)
                    {
                        if (word.wordType == WordType.DictionaryWord && dict.Contains(word.word))
                        {
                            sb.Append(spanBegin + word.word + spanEnd);
                        }
                        else
                            sb.Append(sb);
                    }
                    sw.Write(sb.ToString());
                }
                sw.Write(footer);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Dispose();
                }
            }
        }
    }
}
