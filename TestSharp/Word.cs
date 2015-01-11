using System;
using System.Collections.Generic;
using System.Text;

namespace TestSharp
{
    public enum WordType { DictionaryWord = 1, Delimiter = 2, RF = 3 };

    /// <summary>
    /// хранит само слово и его тип (словарное, знак препинания, стандартный разделитель строк).
    /// </summary>
    public struct Word
    {
        public String word;
        public WordType wordType;
    }
}
