using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSharp
{
    public enum WordType { DictionaryWord = 1, Delimiter = 2, RF = 3 };

    public struct Word
    {
        public String word;
        public WordType wordType;
    }
}
