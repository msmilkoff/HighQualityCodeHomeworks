using System;

namespace Methods
{
    public class Student
    {
        private string firstName;
        private string lastName;
        private string otherInfo;
        
        public DateTime BirthDay { get; set; }

        public string FirstName
        {
            get { return this.firstName; }
            set {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 2)
                {
                    throw new ArgumentException("Invalid first name", nameof(value));
                }

                this.firstName = value;
            }
        }

        public string LastName
        {
            get { return this.lastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 2)
                {
                    throw new ArgumentException("Invalid last name", nameof(value));
                }

                this.lastName = value;
            }
        }

        public string OtherInfo
        {
            get { return this.otherInfo; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Additional student info cannot be emtpy.");
                }

                this.otherInfo = value;
            }
        }
        
        public bool IsOlderThan(Student other)
        {
            bool isThisOlder = this.BirthDay < other.BirthDay;

            return isThisOlder;
        }
    }
}