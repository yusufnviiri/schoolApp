﻿namespace schoolApp.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public Semester Semester { get; set; }
        public SchoolLevel SchoolLevel { get; set; }
        public Assessement Assessement { get; set; } = new Assessement();

    }
}
