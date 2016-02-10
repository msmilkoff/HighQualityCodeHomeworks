using System.Collections.Generic;
using System.Text;

namespace InheritanceAndPolymorphism
{
    public class OffsiteCourse : Course
    {
        public OffsiteCourse(string name) 
            : base(name)
        {
            this.Town = null;
        }

        public OffsiteCourse(string courseName, string teacherName) 
            : base(courseName, teacherName)
        {
            this.Town = null;
        }

        public OffsiteCourse(string courseName, string teacherName, ICollection<string> students) 
            : base(courseName, teacherName, students)
        {
            this.Town = null;
        }

        public string Town { get; set; }

        public override string ToString()
        {
            var output = new StringBuilder();
            output.Append(base.ToString());
            output.AppendLine(this.Town != null 
                ? $"Town: {this.Town}" 
                : "Town: N/A");

            return output.ToString();
        }
    }
}
