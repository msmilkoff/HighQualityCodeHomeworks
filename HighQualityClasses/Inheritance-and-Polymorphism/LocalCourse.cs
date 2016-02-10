using System.Collections.Generic;
using System.Text;

namespace InheritanceAndPolymorphism
{
    public class LocalCourse : Course
    {
        public LocalCourse(string name) 
            : base(name)
        {
            this.Lab = null;
        }

        public LocalCourse(string courseName, string teacherName) : 
            base(courseName, teacherName)
        {
            this.Lab = null;
        }

        public LocalCourse(string courseName, string teacherName, ICollection<string> students) 
            : base(courseName, teacherName, students)
        {
            this.Lab = null;
        }

        public string Lab { get; set; }

        public override string ToString()
        {
            var output = new StringBuilder();
            output.Append(base.ToString());
            output.AppendLine(this.Lab != null 
                ? $"Lab: {this.Lab}" 
                : "Lab: N/A");

            return output.ToString();
        }
    }
}
