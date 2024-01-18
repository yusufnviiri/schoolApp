namespace schoolApp.Models
{
    public class ReportCard
    {
        public int ReportCardId { get; set; }
        public ICollection<Course> Courses { get; set; }   = new List<Course>();
        public SchoolLevel Level { get; set; }

        public Semester Semester { get; set; }
        public int StudentId { get; set; }
    }
}
