using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSharp
{
    class HtmlFileManager:IDisposable
    {
        private SentenceParser sentenceParser;
        private int countLinesInHtml;
        private int countHtmlFile;
        private String directoryName;
        private Sentence lastSentence;
        private Dictionary dict;

        public HtmlFileManager(SentenceParser sentenceParser, int countLinesInHtml, Dictionary dict)
        {
            this.sentenceParser = sentenceParser;
            this.countLinesInHtml = countLinesInHtml;
            countHtmlFile = 0;
            directoryName = String.Empty;
            lastSentence = null;
            this.dict = dict;
        }

        public void Dispose()
        {
            if (this.sentenceParser != null)
            {
                sentenceParser.Dispose();
            }
        }

        public bool EndOfStream
        {
            get
            {
                return (sentenceParser == null || sentenceParser.EndOfStream);
            }
        }


        private String getNextFileName()
        {
            countHtmlFile++;
            return directoryName + countHtmlFile.ToString() + ".html";
        }

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
