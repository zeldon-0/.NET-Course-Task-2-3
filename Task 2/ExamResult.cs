using System;
using System.Text;
namespace TreeImplementation
{
        public class ExamResult:IComparable
    {
        public ExamResult(int score, string name, DateTime date)
        {
            if (score<0 || score>100)
                throw new ArgumentOutOfRangeException("The score is impossible to achieve.");
            Score=score;
            Name=name;
            Date=date;
        }
        public ExamResult(int score, string name)
        {
            if (score<0 || score>100)
                throw new ArgumentOutOfRangeException("The score is impossible to achieve.");
            Score=score;
            Name=name;
            Date=DateTime.Now;
        }

        public int Score {get; private set;}
        public string Name {get; private set;}
        public DateTime Date {get; private set;}
        public int CompareTo(object b)
        {
            ExamResult temp=b as ExamResult;
            if (temp==null) return -1;
            if (Score>temp.Score) return 1;
            else  if (Score==temp.Score && 
            Date==temp.Date &&
            Name==temp.Name) return 0;
            return -1;
        }
        public override string ToString()
        {
            StringBuilder info= new StringBuilder();
            try
            {
                info.Append($"Name: {Name}\n");
                info.Append($"Exam score: {Score}\n");
                info.Append($"Date taken: {Date}\n");
            }
            catch (ArgumentOutOfRangeException)
            {
                info.Clear();
                info.Append($"Exam score: {Score}");
                info.Append($"The other information is too long for storage");
            }
            return info.ToString();
            
        }    
    }

}