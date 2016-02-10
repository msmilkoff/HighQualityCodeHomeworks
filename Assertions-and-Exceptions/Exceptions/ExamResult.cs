using System;

namespace Exceptions_Homework
{
    public class ExamResult
    {
        private int grade;
        private int minGrade;
        private int maxGrade;
        private string comments;

        public ExamResult(int grade, int minGrade, int maxGrade, string comments)
        {
            this.Grade = grade;
            this.MinGrade = minGrade;
            this.MaxGrade = maxGrade;
            this.Comments = comments;
        }

        public int Grade
        {
            get { return this.grade; }
            private set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Grade must be in range [1, 100]");
                }
                this.grade = value;
            }
        }

        public int MinGrade
        {
            get { return this.minGrade; }
            private set
            {
                if (value < 2 || value > 6)
                { 
                    throw new ArgumentOutOfRangeException(nameof(value), "Minimal grade must be in range [2, 6]");
                }
                this.minGrade = value;
            }
        }

        public int MaxGrade
        {
            get { return this.maxGrade; }
            private set
            {
                if (value < this.MinGrade || value > 6)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Max grade must be in range (minGrade, 6]");
                }
                this.maxGrade = value;
            }
        }

        public string Comments
        {
            get { return this.comments; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 10)
                {
                    throw new ArgumentException("Comment must be at least 10 characters long");
                }
                this.comments = value;
            }
        }
    }
}
