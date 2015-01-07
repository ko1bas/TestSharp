using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSharp
{
    class SentenceParser
    {
        private  static const String endOfSentence = ".?!";
      
        List<Sentense> arraySentense;

        public SentenceParser(WordParser wordParser)
        {
            arraySentense = new List<Sentense>();
            Sentense sentence = new Sentense();
            int i = 0;
            while (i<wordParser.getWords().Count)
            {
                if (!isEndOfSentence(wordParser.getWords()[i]))
                {
                    sentence.append(wordParser.getWords()[i]);
                }
                else
                {
                    sentence.append(wordParser.getWords()[i]);
                    if (Sentense.delimeter.Equals(wordParser.getWords()[i + 1]))
                    {
                        i++;
                        sentence.append(wordParser.getWords()[i]);
                    }
                    arraySentense.Add(sentence);
                    sentence = new Sentense();
                }
                i++;
            }
        }


        public bool isEndOfSentence(String word)
        {
            return (endOfSentence.IndexOf(word) >= 0);
        }
    }
}
