using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LabArchitectures.Model
{
    [Serializable()]
    public class Query
    {
        private String filePath;
        private DateTime execDate;
        private int wordCnt;
        private int charCnt;
        private int lineCnt;

        #region getset
        public DateTime ExecDate
        {
            get { return execDate; }
            set { execDate = value; }
        }
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }
        public int WordCnt
        {
            get { return wordCnt; }
            set { wordCnt = value; }
        }
        public int CharCnt
        {
            get { return charCnt; }
            set { charCnt = value; }
        }
        public int LineCnt
        {
            get { return lineCnt; }
            set { lineCnt = value; }
        }
        #endregion
        public override string ToString()
        {
            return "Date: " + execDate + "File: " + filePath + "Symbols: " + CharCnt + " Words: " + WordCnt + " Lines: " + LineCnt;
        }
        public Query(DateTime d, string f, string text)
        {
            filePath = f;
            execDate = d;
            GetResAndWrite(text);
        }
        //reads text and returns statistics
        public string GetRes(string text)
        {

            int CharCount = text.Length;
            int LinesCount = text.Split('\r').Length;
            int WordsCount = Regex.Matches(text, @"\b\w+\b").Count;
            String a = "Symbols: " + CharCount + " Words: " + WordsCount + " Lines: " + LinesCount;
            return a;
        }
        public void GetResAndWrite(string text)
        {

            int CharCount = text.Length;
            int LinesCount = text.Split('\r').Length;
            int WordsCount = Regex.Matches(text, @"\b\w+\b").Count;
            // String a = "Symbols: " + CharCount + " Words: " + WordsCount + " Lines: " + LinesCount;
            WordCnt = WordsCount;
            CharCnt = CharCount;
            LineCnt = LinesCount;

        }

    }
}
