using System;

namespace Product
{
    public class Student
    {
        string lastName, firstName, group;

        public string LastName
        {
            get => lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException($"{nameof(value)} is null");
                }

                lastName = value;
            }
        }

        public string FirstName 
        {
            get => firstName; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException($"{nameof(value)} is null");
                }

                firstName = value;
            }
        }

        public string Group
        {
            get => group;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException($"{nameof(value)} is null");
                }

                group = value;
            }
        }

        public Student(string surname, string name, string group)
        {
            LastName = surname;
            FirstName = name;
            Group = group;
        }
    }
}
