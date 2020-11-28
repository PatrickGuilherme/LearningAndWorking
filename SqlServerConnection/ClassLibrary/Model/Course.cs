using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<StudentToCourse> StudentsToCourses { get; set; }
    }
}
