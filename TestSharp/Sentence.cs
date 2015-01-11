using System;
using System.Collections.Generic;
using System.Text;

namespace TestSharp
{
    /// <summary>
    /// Хранит одно предложение текста. Предложение может занимать несколько строк.
    /// Текст разбит на слова. Словам сопоставлен предполагаемый тип (возможное словарное слово, знак препинания, разделитель строки).
    /// Ведется счетчик строк, которое занимает предложение.
    /// </summary>
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

        /// <summary>
        /// Добавляет слово в предложение.
        /// </summary>
        /// <param name="word"></param>
        public void append(Word word)
        {
            words.Add(word);
            if (word.wordType.Equals(WordType.RF))
            {
                countRF++;
            }
            count++;
        }

        /// <summary>
        /// Возвращает количество слов в предложении.
        /// </summary>
        public int Count
        {
            get
            {
                return words.Count;
            }
        }

        /// <summary>
        /// Возвращает список слов в предложении.
        /// </summary>
        public List<Word> Words
        {
            get
            {
                return words;
            }
        }

    }
}
