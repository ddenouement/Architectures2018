using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LabArchitectures.Model
{
  public  class Query
    {
        private String _filePath;
        private DateTime _execDate;

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
        public override string ToString()
        {
            return "Date: "+_execDate + "File: " + _filePath;
        }
        public Query(DateTime d , string f , User user)  {
            _filePath = f;
            _execDate = d;
          //   user.AddQ(this); no need
        }
        //reads text and returns staistics
        public static string GetRes(string text)
        {
             
            int CharCount = text.Length;            
            int LinesCount = text.Split('\r').Length;       
            int WordsCount =  Regex.Matches(text, @"\b\w+\b").Count;//text.Split(' ').Length; 
            String a = "Symbols: "+CharCount+" Words: "+WordsCount+" Lines: "+LinesCount;
            return a;
        }

    }
}
