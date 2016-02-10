using System;
using System.Collections.Generic;
using System.Linq;

namespace Exceptions_Homework
{
    public class Student
    {
        private string firstName;
        private string lastName;

        public Student(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Exams = new List<IExam>();
        }

        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("First name cannot be empty", nameof(value));
                }
                this.firstName = value;
            }
        }

        public string LastName
        {
            get { return this.lastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Last name cannot be empty", nameof(value));
                }
                this.lastName = value;
            }
        }

        public IList<IExam> Exams { get; set; }

        public IList<ExamResult> CheckExams()
        {
            CheckForNullOrEmpty(this.Exams);

            var examResults = this.Exams.
                Select(exam => exam.Check())
                .ToList();

            return examResults;
        }

        public double CalcAverageExamResultInPercents()
        {
            CheckForNullOrEmpty(this.Exams);

            double[] examScore = new double[this.Exams.Count];
            IList<ExamResult> examResults = CheckExams();
            for (int i = 0; i < examResults.Count; i++)
            {
                examScore[i] = 
                    ((double)examResults[i].Grade - examResults[i].MinGrade) / 
                    (examResults[i].MaxGrade - examResults[i].MinGrade);
            }

            return examScore.Average();
        }

        private void CheckForNullOrEmpty(IList<IExam> examList)
        {
            if (examList == null || examList.Count == 0)
            {
                throw new InvalidOperationException("Currently there are no exams inthe database");
            }
        }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }
    }
}
