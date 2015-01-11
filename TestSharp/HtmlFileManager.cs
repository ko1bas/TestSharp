using System;
using System.Collections.Generic;
using System.Text;

namespace TestSharp
{
    /// <summary>
    /// Разбивает предложения, полученные от SentenceParser, согласно заданию для сохранения в html-файл.
    /// Заведует генерацией имен html-файлов.
    /// Сохраняет html-файлы.
    /// </summary>
    class HtmlFileManager:IDisposable
    {
        private SentenceParser sentenceParser;
        private int countLinesInHtml;
        private int countHtmlFile;
        private String directoryName; //мождо добавить get и set 
        private Sentence lastSentence;
        private Dictionary dict;

        /// <summary>
        /// </summary>
        /// <param name="sentenceParser"></param>
        /// <param name="countLinesInHtml">максимальное количество строк в html-файле.
        ///</param>
        /// <param name="dict"></param>
        public HtmlFileManager(SentenceParser sentenceParser, int countLinesInHtml, Dictionary dict)
        {
            this.sentenceParser = sentenceParser;
            this.countLinesInHtml = countLinesInHtml;
            countHtmlFile = 0;
            directoryName = String.Empty;
            lastSentence = null;
            this.dict = dict;
        }

        /// <summary>
        /// Освобождаем ресурсы. 
        /// </summary>
        public void Dispose()
        {
            if (this.sentenceParser != null)
            {
                sentenceParser.Dispose();
            }
        }

        /// <summary>
        /// Возвращает true, если достигнут конец считывания предложений.
        /// </summary>
        public bool EndOfStream
        {
            get
            {
                return (sentenceParser == null || sentenceParser.EndOfStream);
            }
        }

        /// <summary>
        /// Генерирует имя для нового html-файла.
        /// </summary>
        /// <returns></returns>
        private String getNextFileName()
        {
            countHtmlFile++;
            return directoryName + countHtmlFile.ToString() + ".html";
        }

        /// <summary>
        /// Сохраняет предложения, которые занимают не более countLinesInHtml строк в html-файл.
        /// </summary>
        /// <returns></returns>
        public bool SaveNextHtmlFile()
        {
            List<Sentence> listSentence = new List<Sentence>();
            if (lastSentence != null && lastSentence.Count > 0)
            {
                listSentence.Add(lastSentence);
                lastSentence = null;
            }

            int count = 0;
            while (!this.EndOfStream && count < countLinesInHtml)
            {
                    Sentence sentence = sentenceParser.getNextSentence();
                    listSentence.Add(sentence);
                    if (sentence.Count>0)
                    {
                        count+=sentence.countRF;
                    }
            }
            if (count > 0)
            {
                if (count > countLinesInHtml)
                {
                    int lastInd = listSentence.Count-1;
                    if (listSentence[lastInd].countRF < countLinesInHtml)
                    {
                        lastSentence = listSentence[lastInd];
                        listSentence.RemoveAt(lastInd);
                    }
                }

                HtmlConvert htmlConvert = new HtmlConvert(listSentence, dict);
                htmlConvert.Save(this.getNextFileName());
                return true;
            }
            return false;
        }



        

    }
}
