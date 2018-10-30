using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LabArchitectures.Model
{
    public class Query
    {
        private String _filePath;
        private DateTime _execDate;
        private int _wordCnt;
        private int _charCnt;
        private int _lineCnt;

        #region getset
        public DateTime ExecDate
        {
            get { return _execDate; }
            set { _execDate = value; }
        }
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }
        public int WordCnt
        {
            get { return _wordCnt; }
            set { _wordCnt = value; }
        }
        public int CharCnt
        {
            get { return _charCnt; }
            set { _charCnt = value; }
        }
        public int LineCnt
        {
            get { return _lineCnt; }
            set { _lineCnt = value; }
        }
        #endregion
        public override string ToString()
        {
            return "Date: " + _execDate + "File: " + _filePath + "Symbols: " + CharCnt + " Words: " + WordCnt + " Lines: " + LineCnt;
        }
        public Query(DateTime d, string f, string text)
        {
            _filePath = f;
            _execDate = d;
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
