using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSharp
{
    class Sentence
    {
        private List<Word> words;
        private int count;
        public static readonly String RF = "\r\n";
        public int countRF;
      

        public Sentence()
        {
            words = new List<Word>();
            countRF = 0;
        }

        public void append(Word word)
        {
            words.Add(word);
            if (word.wordType.Equals(WordType.RF))
            {
                countRF++;
            }
            count++;
        }

        public int Count
        {
            get
            {
                return words.Count;
            }
        }

        public List<Word> Words
        {
            get
            {
                return words;
            }
        }

    }
}
