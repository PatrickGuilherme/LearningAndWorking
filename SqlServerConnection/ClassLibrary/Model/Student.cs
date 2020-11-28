using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public DateTime DateBirth { get; set; }
        public List<StudentToCourse> StudentsToCourses { get; set; }
    }
}
