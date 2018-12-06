using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LabArchitectures.DB;

namespace LabArchitectures.Model
{
    [Serializable()]
    public class Query
    {
        public Guid Id { get; set; }
        private String filePath;
        private DateTime execDate;
        private int wordCnt;
        private int charCnt;
        private int lineCnt;
        private User _user;

        #region Properties
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

        public Guid UserId { get; set; }
    
        public User User
        {
            get { return _user; }
            set { _user = value; }
        }
        #endregion


        #region EntityConfiguration

        public class QueryEntityConfiguration: EntityTypeConfiguration<Query>
        {
          public  QueryEntityConfiguration()
            {
                ToTable("Queries");
                HasKey(q => q.Id);
                
                Property(q => q.FilePath).HasColumnName("FilePath").IsRequired();
                Property(q => q.ExecDate).HasColumnName("ExecDate").IsRequired();
                Property(q => q.WordCnt).HasColumnName("WordCnt").IsRequired();
                Property(q => q.CharCnt).HasColumnName("CharCnt").IsRequired();
                Property(q => q.LineCnt).HasColumnName("LineCnt").IsRequired();

                HasRequired(tr => tr.User)
                    .WithMany(u => u.Queries)
                    .HasForeignKey(tr => tr.UserId);
            }
        }

        #endregion


        public override string ToString()
        {
            return "Date: " + execDate + "File: " + filePath + "Symbols: " + CharCnt + " Words: " + WordCnt + " Lines: " + LineCnt;
        }
        public Query()
        {

        }
        public Query(DateTime d, string f, string text, Guid user)
        {
            Id = Guid.NewGuid();
            FilePath = f;
            ExecDate = d;
            GetResAndWrite(text);
            UserId = user;
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
