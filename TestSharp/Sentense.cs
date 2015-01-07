using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSharp
{
    class Sentense
    {
        private List<String> words;
        public static const String delimeter = "\n";

        public Sentense()
        {
            words = new List<String>();
        }

        public void append(String word)
        {
            words.Add(word);
        }


        public bool EndsRF()
        {
            return delimeter.Equals(words[words.Count - 1]);
        }
    }
}
