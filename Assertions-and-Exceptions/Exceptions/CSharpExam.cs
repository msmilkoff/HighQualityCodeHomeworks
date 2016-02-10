using System;

namespace Exceptions_Homework
{
    public class CSharpExam : IExam
    {
        private int score;

        public CSharpExam(int score)
        {
            this.Score = score;
        }

        public int Score
        {
            get { return this.score; }
            private set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Score should be in range [0, 100]");
                }
                this.score = value;
            }
        }

        public ExamResult Check()
        {
            var examResult = new ExamResult(ToGrade(this.Score), 2, 6, "Exam results calculated by score.");

            return examResult;
        }

        private int ToGrade(int resultScore)
        {
            int grade = 2;

            if (resultScore > 50 && resultScore <= 60)
            {
                grade = 3;
            }
            else if (resultScore > 60 && resultScore <= 70)
            {
                grade = 4;
            }
            else if (resultScore < 70 && resultScore <= 80)
            {
                grade = 5;
            }
            else if (resultScore > 80)
            {
                grade = 6;
            }

            return grade;
        }
    }
}
