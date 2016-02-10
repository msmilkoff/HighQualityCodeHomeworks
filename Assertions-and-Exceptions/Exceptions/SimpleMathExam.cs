using System;

namespace Exceptions_Homework
{
    public class SimpleMathExam : IExam
    {
        private int problemsSolved;

        public SimpleMathExam(int problemsSolved)
        {
            this.ProblemsSolved = problemsSolved;
        }

        public int ProblemsSolved
        {
            get { return this.problemsSolved; }
            private set
            {
                if (value < 0 || value > 10)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        "The amount of solved problems cannot be negative or greater than 10");
                }
                this.problemsSolved = value;
            }
        }

        public ExamResult Check()
        {
            int solvedProblems = this.ProblemsSolved;

            switch (solvedProblems)
            {
                case 0:
                    return new ExamResult(2, 2, 6, $"Bad result. Problems solved: {solvedProblems}");
                case 1:
                    return new ExamResult(4, 2, 6, $"Average result. Problems solved: {solvedProblems}");
                case 2:
                    return new ExamResult(6, 2, 6, "Average result: nothing done.");
            }

            throw new InvalidOperationException($"Current exam does not have {solvedProblems} problems");
        }
    }
}
