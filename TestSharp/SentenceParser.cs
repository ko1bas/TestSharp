using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSharp
{
    class SentenceParser : IDisposable
    {
        private static readonly String endOfSentence = ".?!";
        public static readonly String Delemiters = " :;\"(),+-=<>?!@#$%^&*~'./\\";
        private WordParser wordParser;
        private List<String> words;


        public SentenceParser(WordParser wordParser)
        {
            this.wordParser = wordParser;
            this.words = new List<String>();
        }

        public bool EndOfStream
        {
            get
            {
                return (wordParser==null || (wordParser.EndOfStream && words.Count.Equals(0)));
            }
        }

        public void Dispose()
        {
            if (!wordParser.Equals(null))
            {
                wordParser.Dispose();
            }
        }

        public Sentence getNextSentence()
        {
            Word tmpWord;
            bool flag = false;
            Sentence sentence = new Sentence();
            while (!flag)
            {
                if (!(wordParser.Equals(null) || wordParser.EndOfStream))
                {
                    words.AddRange(wordParser.ReadLine());
                }

                if (words.Count.Equals(0))
                    break;

                int i = 0;
                while (i < words.Count)
                {
                    tmpWord.word = words[i];
                    if (!this.isEndOfSentence(words[i]))
                    {
                        if (Sentence.RF.Equals(words[i]))
                        {
                            tmpWord.wordType = WordType.RF;
                            sentence.append(tmpWord);
                        }
                        else
                        {
                            if (this.isDelimiters(words[i]))
                                tmpWord.wordType = WordType.Delimiter;
                            else
                                tmpWord.wordType = WordType.DictionaryWord;

                            sentence.append(tmpWord);
                        }
                    }
                    else
                    {
                        tmpWord.wordType = WordType.Delimiter;
                        sentence.append(tmpWord);
                        int j=i+1;
                        while (this.isEndOfSentence(words[j]))
                        {
                            tmpWord.word = words[j];
                            tmpWord.wordType = WordType.Delimiter;
                            sentence.append(tmpWord);
                            j++;
                        }
                        words.RemoveRange(0,  j);
                        flag = true;
                        break;

                    }
                    i++;
                }
                words.Clear();
            }
            return sentence;
        }

        public bool isDelimiters(String word)
        {
            return (word.Length == 1 && Delemiters.IndexOf(word) >= 0);
        }
        public bool isEndOfSentence(String word)
        {
            return (endOfSentence.IndexOf(word) >= 0);
        }
    }
}
