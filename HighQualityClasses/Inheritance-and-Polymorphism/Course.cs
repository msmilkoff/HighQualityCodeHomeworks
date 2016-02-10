namespace InheritanceAndPolymorphism
{
    using System.Collections.Generic;
    using System.Text;

    public abstract class Course
    {
        protected Course(string name)
        {
            this.Name = name;
            this.TeacherName = null;
            this.Students = new List<string>();
        }

        protected Course(string courseName, string teacherName)
        {
            this.Name = courseName;
            this.TeacherName = teacherName;
            this.Students = new List<string>();
        }

        protected Course(string courseName, string teacherName, ICollection<string> students)
        {
            this.Name = courseName;
            this.TeacherName = teacherName;
            this.Students = students;
        }

        public string Name { get; set; }

        public string TeacherName { get; set; }

        public ICollection<string> Students { get; set; }
        
        protected string GetStudentsAsString()
        {
            if (this.Students == null || this.Students.Count == 0)
            {
                return "{ }";
            }

            return "{ " + string.Join(", ", this.Students) + " }";
        }

        public override string ToString()
        {
            var output = new StringBuilder();

            output.AppendLine($"Course type: {this.GetType().Name}");
            output.AppendLine($"Course name: {this.Name}");
            output.AppendLine(this.TeacherName != null 
                ? $"Teacher: {this.TeacherName}" 
                : "Teacher: N/A");
            output.AppendLine($"Students: {GetStudentsAsString()}");

            return output.ToString();
        }
    }
}
